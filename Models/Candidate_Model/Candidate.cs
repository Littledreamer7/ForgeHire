using System.ComponentModel.DataAnnotations;

namespace ForgeHire.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(15)]
        public string MobileNumber { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        // 🔥 MAKE OPTIONAL
        public int? YearsOfExperience { get; set; }

        [MaxLength(100)]
        public string? CurrentCompany { get; set; }

        [MaxLength(100)]
        public string? CurrentRole { get; set; }

        public string? Skills { get; set; }

        public string? ResumeUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}