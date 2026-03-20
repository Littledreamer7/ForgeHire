using ForgeHire.Auth.Auth_Dtos;
using ForgeHire.Helpers.Common;

namespace ForgeHire.Services.IServices
{
    public interface IOtpService
    {
        Task<ApiResponse> SendOtpAsync(SendOtpDto dto);

        Task<AuthResponseDto> VerifyOtpAsync(VerifyOtpDto dto);
    }
}