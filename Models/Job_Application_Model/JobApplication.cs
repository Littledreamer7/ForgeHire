using ForgeHire.Enums;
using ForgeHire.Models.Job_Model;

namespace ForgeHire.Models.Job_Application_Model
{
        public class JobApplication
        {
            public int Id { get; set; }

            public int JobId { get; set; }
            public Job Job { get; set; }

            public int CandidateId { get; set; }
            public User Candidate { get; set; }

            public string ResumeUrl { get; set; }

            public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;

            public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
        }
   
}
