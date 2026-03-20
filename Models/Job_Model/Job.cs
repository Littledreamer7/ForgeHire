using ForgeHire.Models.Company_Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForgeHire.Models.Job_Model
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company_Info Company { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        public string Department { get; set; }

        public string Location { get; set; }

        public string JobType { get; set; }

        public string SalaryRange { get; set; }

        public string Status { get; set; } = "Active";

        public int ApplicantsCount { get; set; } = 0;

        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        public string Description { get; set; }
    }
}