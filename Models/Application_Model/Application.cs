using ForgeHire.Models.Job_Model;

namespace ForgeHire.Models.Application_Model
{
    public class Application
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }   // 🔥 TENANT ID
        public int JobId { get; set; }
        public int CandidateId { get; set; }

        public Job Job { get; set; }
    }
}
