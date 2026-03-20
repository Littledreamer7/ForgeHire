using System.ComponentModel.DataAnnotations;

namespace ForgeHire.Dtos.Job_Dtos
{
    public class CreateJobDto
    {
        [Required]
        public string Title { get; set; }

        public string Department { get; set; }

        public string Location { get; set; }

        public string JobType { get; set; }

        public string SalaryRange { get; set; }

        public string Description { get; set; }
    }
}