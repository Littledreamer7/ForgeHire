using System.ComponentModel.DataAnnotations;

namespace ForgeHire.Auth.Auth_Dtos
{
    public class VerifyOtpDto
    {
        [Required]
        public string MobileNumber { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6)]
        public string Otp { get; set; }
    }
}