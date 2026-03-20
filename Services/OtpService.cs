//using ForgeHire.Auth.Auth_Dtos;
//using ForgeHire.Auth.Auth_Services;
//using ForgeHire.Data;
//using ForgeHire.Helpers;
//using ForgeHire.Helpers.Common;
//using ForgeHire.Models;
//using ForgeHire.Services.IServices;
//using Microsoft.EntityFrameworkCore;

//namespace ForgeHire.Services
//{
//    public class OtpService : IOtpService
//    {
//        private readonly AppDbContext _db;
//        private readonly Auth_JwtService _jwt;
//        private readonly ISmsService _sms;

//        public OtpService(AppDbContext db, Auth_JwtService jwt, ISmsService sms)
//        {
//            _db = db;
//            _jwt = jwt;
//            _sms = sms;
//        }

//        // =========================
//        // SEND OTP
//        // =========================
//        public async Task<ApiResponse> SendOtpAsync(SendOtpDto dto)
//        {
//            if (!PhoneHelper.IsValidIndianMobile(dto.MobileNumber))
//                return new ApiResponse(false, "Invalid mobile number");

//            var mobile = PhoneHelper.NormalizeMobile(dto.MobileNumber);

//            var userExists = await _db.Users
//                .AnyAsync(x => x.MobileNumber == mobile);

//            if (!userExists)
//                return new ApiResponse(false, "User not registered");

//            var oldOtps = _db.OtpRecords
//                .Where(x => x.MobileNumber == mobile);

//            _db.OtpRecords.RemoveRange(oldOtps);

//            var otp = GenerateOtp();

//            _db.OtpRecords.Add(new OtpRecord
//            {
//                MobileNumber = mobile,
//                OtpHash = SecurityHelper.Hash(otp),
//                ExpiryTime = DateTime.UtcNow.AddMinutes(5),
//                CreatedAt = DateTime.UtcNow,
//                IsUsed = false,
//                AttemptCount = 0
//            });

//            await _db.SaveChangesAsync();

//            await _sms.SendSmsAsync(
//                mobile,
//                $"Your ForgeHire OTP is {otp}. Valid for 5 minutes."
//            );

//            return new ApiResponse(true, "OTP sent successfully");
//        }

//        // =========================
//        // VERIFY OTP
//        // =========================
//        public async Task<AuthResponseDto> VerifyOtpAsync(VerifyOtpDto dto)
//        {
//            if (!PhoneHelper.IsValidIndianMobile(dto.MobileNumber))
//                throw new Exception("Invalid mobile number");

//            var mobile = PhoneHelper.NormalizeMobile(dto.MobileNumber);

//            var record = await _db.OtpRecords
//                .Where(x => x.MobileNumber == mobile && !x.IsUsed)
//                .OrderByDescending(x => x.Id)
//                .FirstOrDefaultAsync();

//            if (record == null)
//                throw new Exception("Invalid OTP request");

//            if (record.ExpiryTime < DateTime.UtcNow)
//                throw new Exception("OTP expired");

//            var hashInput = SecurityHelper.Hash(dto.Otp);

//            if (record.OtpHash != hashInput)
//            {
//                record.AttemptCount++;

//                if (record.AttemptCount >= 5)
//                    throw new Exception("Too many invalid attempts");

//                await _db.SaveChangesAsync();
//                throw new Exception("Invalid OTP");
//            }

//            record.IsUsed = true;
//            await _db.SaveChangesAsync();

//            var user = await _db.Users
//                .FirstOrDefaultAsync(x => x.MobileNumber == mobile);

//            if (user == null)
//                throw new Exception("User not found");

//            var companyUser = await _db.CompanyUsers
//                .FirstOrDefaultAsync(x => x.UserId == user.Id && x.IsActive);

//            string role;
//            int? companyId = null;

//            if (companyUser != null)
//            {
//                role = companyUser.Role;
//                companyId = companyUser.CompanyId;
//            }
//            else
//            {
//                var candidate = await _db.Candidates
//                    .FirstOrDefaultAsync(x => x.MobileNumber == mobile);

//                if (candidate == null)
//                    throw new Exception("User not associated with any company");

//                role = "Candidate";
//            }

//            var accessToken = _jwt.GenerateAccessToken(
//                user.Id,
//                mobile,
//                role,
//                companyId
//            );

//            return new AuthResponseDto
//            {
//                AccessToken = accessToken,
//                Role = role,
//                UserId = user.Id
//            };
//        }

//        // =========================
//        // OTP GENERATOR
//        // =========================
//        private static readonly Random _random = new Random();

//        private string GenerateOtp()
//        {
//            return _random.Next(100000, 999999).ToString();
//        }
//    }
//}