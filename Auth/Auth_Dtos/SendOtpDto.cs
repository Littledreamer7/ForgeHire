using System.ComponentModel.DataAnnotations;

namespace ForgeHire.Auth.Auth_Dtos
{
    public class SendOtpDto
    {
        [Required]
        [RegularExpression(@"^\+91[6-9]\d{9}$",
            ErrorMessage = "Invalid Indian mobile number")]
        public string MobileNumber { get; set; }
    }
}
