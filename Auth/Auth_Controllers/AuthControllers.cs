using ForgeHire.Auth.Auth_Dtos;
using ForgeHire.Auth.Auth_Services;
using Microsoft.AspNetCore.Mvc;

namespace ForgeHire.Auth.Auth_Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class Auth_Controllers : ControllerBase
    {
        private readonly IAuthService _service;

        public Auth_Controllers(IAuthService service)
        {
            _service = service;
        }

        // ======================
        // SEND OTP
        // ======================
        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpDto dto)
        {
            await _service.SendOtpAsync(dto);
            return Ok(new { message = "OTP sent successfully" });
        }

        // ======================
        // VERIFY OTP (LOGIN)
        // ======================
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto dto)
        {
            var result = await _service.VerifyOtpAsync(dto, null);

            //SET REFRESH TOKEN COOKIE
            SetRefreshTokenCookie(result.RefreshToken);

            //Hide refresh token from response
            result.RefreshToken = null;

            return Ok(result);
        }

        // ======================
        // REFRESH TOKEN
        // ======================
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var token = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Refresh token missing" });

            var result = await _service.RefreshAsync(token);

            //Rotate refresh token
            SetRefreshTokenCookie(result.RefreshToken);

            result.RefreshToken = null;

            return Ok(result);
        }

        // ======================
        // LOGOUT
        // ======================
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Cookies["refreshToken"];

            if (!string.IsNullOrEmpty(token))
            {
                await _service.LogoutAsync(token);
            }

            Response.Cookies.Delete("refreshToken");

            return Ok(new { message = "Logged out successfully" });
        }

        // ======================
        // PRIVATE HELPER
        // ======================
        private void SetRefreshTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // change to TRUE in production (HTTPS)
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}