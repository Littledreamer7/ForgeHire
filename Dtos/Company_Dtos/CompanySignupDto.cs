using System.ComponentModel.DataAnnotations;

namespace ForgeHire.Dtos.Company_Dtos
{
    public class CompanySignUpDto
    {
        [Required(ErrorMessage = "Company name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Company name must be between 2 and 100 characters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [RegularExpression(@"^(?:\+91)?[6-9]\d{9}$", ErrorMessage = "Mobile number must be a valid Indian number")]
        public string MobileNumber { get; set; }

        [RegularExpression(@"^[0-9A-Z]{15}$", ErrorMessage = "Invalid GST number format")]
        public string? GstNumber { get; set; }

        //[RegularExpression(@"^[A-Z]{3}\d{5}[A-Z]{1}\d{4}[A-Z]{3}\d{6}$", ErrorMessage = "Invalid CIN number format")]
        public string? CinNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Location must be between 2 and 150 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Industry type is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Industry type must be between 2 and 100 characters")]
        public string IndustryType { get; set; }

        [Url(ErrorMessage = "Website must be a valid URL")]
        [MaxLength(200, ErrorMessage = "Website cannot exceed 200 characters")]
        public string? Website { get; set; }

        [RegularExpression(@"^(1-10|10-50|50-200|200-500|500\+)$",
         ErrorMessage = "Company size must be one of: 1-10, 10-50, 50-200, 200-500, 500+")]
        public string? CompanySize { get; set; }
    }
}