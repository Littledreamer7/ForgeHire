//using ForgeHire.Data;
//using ForgeHire.Helpers;
//using ForgeHire.Models;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace ForgeHire.Services
//{
//    public class _JwtService
//    {
//        private readonly IConfiguration _config;
//        private readonly AppDbContext _db;

//        public _JwtService(IConfiguration config, AppDbContext db)
//        {
//            _config = config;
//            _db = db;
//        }

//        public string GenerateAccessToken(User user)
//        {
//            var claims = new[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim("mobile", user.MobileNumber),
//                new Claim(ClaimTypes.Role, user.Role.ToString())
//            };

//            var key = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
//            );

//            var creds = new SigningCredentials(
//                key,
//                SecurityAlgorithms.HmacSha256
//            );

//            var token = new JwtSecurityToken(
//                issuer: _config["Jwt:Issuer"],
//                audience: _config["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.UtcNow.AddMinutes(
//                    int.Parse(_config["Jwt:AccessTokenExpiryMinutes"])
//                ),
//                signingCredentials: creds
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

//        public async Task<string> GenerateRefreshToken(User user)
//        {
//            var refreshToken = SecurityHelper.GenerateSecureToken();

//            var refreshTokenHash = SecurityHelper.Hash(refreshToken);

//            _db.RefreshTokens.Add(new RefreshToken
//            {
//                UserId = user.Id,
//                TokenHash = refreshTokenHash,
//                ExpiryDate = DateTime.UtcNow.AddDays(
//                    int.Parse(_config["Jwt:RefreshTokenExpiryDays"])
//                )
//            });

//            await _db.SaveChangesAsync();

//            return refreshToken;
//        }
//    }
//}