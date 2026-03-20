using ForgeHire.Models.Job_Model;
using ForgeHire.Models.User_Model;
using System.ComponentModel.DataAnnotations;

namespace ForgeHire.Models.Company_Model
{
    public class Company_Info
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string CompanyName { get; set; }

        [Required, MaxLength(13)]
        public string MobileNumber { get; set; }

        [MaxLength(15)]
        public string? GstNumber { get; set; }

        [MaxLength(21)]
        public string? CinNumber { get; set; }

        [MaxLength(150), EmailAddress]
        public string? Email { get; set; }

        [Required, MaxLength(150)]
        public string Location { get; set; }

        [Required, MaxLength(100)]
        public string IndustryType { get; set; }

        [MaxLength(200)]
        public string? Website { get; set; }

        [MaxLength(50)]
        public string? CompanySize { get; set; }

        public bool IsVerified { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //NAVIGATION
        public ICollection<Job> Jobs { get; set; } = new List<Job>();

        public ICollection<CompanyUser> Users { get; set; } = new List<CompanyUser>();
    }
}