using ForgeHire.Auth.Auth_Dtos;
using ForgeHire.Data;
using ForgeHire.Models;
using ForgeHire.Models.User_Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace ForgeHire.Auth.Auth_Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly Auth_JwtService _jwt;

        public AuthService(AppDbContext db, Auth_JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        // ======================
        // NORMALIZE MOBILE (FIXED)
        // ======================
        private string NormalizeMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                return mobile;

            mobile = mobile.Trim();
            mobile = mobile.Replace(" ", "").Replace("-", "");

            // already +91 → keep
            if (mobile.StartsWith("+91"))
                return mobile;

            // 10 digit → convert to +91
            if (mobile.Length == 10)
                return "+91" + mobile;

            return mobile;
        }

        // ======================
        // GENERATE OTP
        // ======================
        private string GenerateOtp()
        {
            var bytes = RandomNumberGenerator.GetBytes(4);
            var number = BitConverter.ToUInt32(bytes, 0) % 900000 + 100000;
            return number.ToString();
        }

        // ======================
        // SEND OTP
        // ======================
        public async Task SendOtpAsync(SendOtpDto dto)
        {
            var mobile = NormalizeMobile(dto.MobileNumber);

            var otp = GenerateOtp();

            var record = new OtpRecord
            {
                MobileNumber = mobile,
                OtpHash = BCrypt.Net.BCrypt.HashPassword(otp),
                ExpiryTime = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false,
                AttemptCount = 0,
                CreatedAt = DateTime.UtcNow
            };

            _db.OtpRecords.Add(record);
            await _db.SaveChangesAsync();

            Console.WriteLine($"OTP for {mobile}: {otp}");
        }

        // ======================
        // VERIFY OTP
        // ======================
        public async Task<AuthResponseDto> VerifyOtpAsync(VerifyOtpDto dto, string ip)
        {
            var mobile = NormalizeMobile(dto.MobileNumber);

            // ======================
            // STEP 1: GET OTP
            // ======================
            var record = await _db.OtpRecords
                .Where(x => x.MobileNumber == mobile && !x.IsUsed)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            if (record == null || record.ExpiryTime < DateTime.UtcNow)
                throw new Exception("OTP invalid or expired");

            // ======================
            // STEP 2: VALIDATE OTP
            // ======================
            if (!BCrypt.Net.BCrypt.Verify(dto.Otp, record.OtpHash))
            {
                record.AttemptCount++;

                if (record.AttemptCount >= 5)
                    throw new Exception("Too many invalid attempts");

                await _db.SaveChangesAsync();
                throw new Exception("Invalid OTP");
            }

            record.IsUsed = true;
            await _db.SaveChangesAsync();

            // ======================
            // STEP 3: GET USER
            // ======================
            var user = await _db.Users
                .FirstOrDefaultAsync(x => x.MobileNumber.Trim() == mobile);

            if (user == null)
                throw new Exception($"User not found for mobile: {mobile}");

            // ======================
            // STEP 4: GET ROLE
            // ======================
            var companyUser = await _db.CompanyUsers
                .FirstOrDefaultAsync(x => x.UserId == user.Id && x.IsActive);

            string role;
            int userId = user.Id;
            int? companyId = null;

            if (companyUser != null)
            {
                role = companyUser.Role;
                companyId = companyUser.CompanyId;
            }
            else
            {
                var candidate = await _db.Candidates
                    .FirstOrDefaultAsync(x => x.MobileNumber == mobile);

                if (candidate == null)
                    throw new Exception("User not associated with any company or candidate");

                role = "Candidate";
            }

            // ======================
            // STEP 5: GENERATE TOKENS
            // ======================
            var accessToken = _jwt.GenerateAccessToken(userId, mobile, role, companyId);
            var refreshToken = _jwt.GenerateRefreshToken();

            // Revoke old tokens
            var oldTokens = await _db.RefreshTokens
                .Where(x => x.UserId == userId && !x.IsRevoked)
                .ToListAsync();

            foreach (var t in oldTokens)
                t.IsRevoked = true;

            // Save new refresh token
            _db.RefreshTokens.Add(new RefreshToken
            {
                TokenHash = _jwt.HashToken(refreshToken),
                UserId = userId,
                Role = role,
                CompanyId = companyId,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false
            });

            await _db.SaveChangesAsync();

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Role = role,
                UserId = userId
            };
        }

        // ======================
        // REFRESH TOKEN
        // ======================
        public async Task<AuthResponseDto> RefreshAsync(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw new Exception("Refresh token is required");

            var hashed = _jwt.HashToken(refreshToken);

            var stored = await _db.RefreshTokens
                .FirstOrDefaultAsync(x =>
                    x.TokenHash == hashed &&
                    !x.IsRevoked &&
                    x.ExpiryDate > DateTime.UtcNow);

            if (stored == null)
                throw new Exception("Invalid refresh token");

            stored.IsRevoked = true;

            var user = await _db.Users.FindAsync(stored.UserId);

            if (user == null)
                throw new Exception("User not found");

            var mobile = user.MobileNumber;

            var newAccess = _jwt.GenerateAccessToken(
                stored.UserId,
                mobile,
                stored.Role,
                stored.CompanyId
            );

            var newRefresh = _jwt.GenerateRefreshToken();

            _db.RefreshTokens.Add(new RefreshToken
            {
                TokenHash = _jwt.HashToken(newRefresh),
                UserId = stored.UserId,
                Role = stored.Role,
                CompanyId = stored.CompanyId,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false
            });

            await _db.SaveChangesAsync();

            return new AuthResponseDto
            {
                AccessToken = newAccess,
                RefreshToken = newRefresh,
                Role = stored.Role,
                UserId = stored.UserId
            };
        }

        // ======================
        // LOGOUT
        // ======================
        public async Task LogoutAsync(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return;

            var hashed = _jwt.HashToken(refreshToken);

            var token = await _db.RefreshTokens
                .FirstOrDefaultAsync(x => x.TokenHash == hashed);

            if (token != null)
            {
                token.IsRevoked = true;
                await _db.SaveChangesAsync();
            }
        }
    }
}