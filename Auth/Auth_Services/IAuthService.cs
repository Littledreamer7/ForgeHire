using ForgeHire.Auth.Auth_Dtos;

namespace ForgeHire.Auth.Auth_Services
{
    public interface IAuthService
    {
        Task SendOtpAsync(SendOtpDto dto);
        Task<AuthResponseDto> VerifyOtpAsync(VerifyOtpDto dto, string ip);
        Task<AuthResponseDto> RefreshAsync(string refreshToken);
        Task LogoutAsync(string refreshToken);
    }
}