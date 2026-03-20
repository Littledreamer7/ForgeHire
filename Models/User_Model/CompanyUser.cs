using ForgeHire.Models.Company_Model;

namespace ForgeHire.Models.User_Model
{

    public class CompanyUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }

        public string Role { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        //Navigation
        public User User { get; set; }
        public Company_Info Company { get; set; }
    }
}