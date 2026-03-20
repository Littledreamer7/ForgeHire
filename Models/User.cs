using ForgeHire.Enums;
using ForgeHire.Models.User_Model;
using System.ComponentModel.DataAnnotations;

namespace ForgeHire.Models
{
        public class User
        {
            public int Id { get; set; }

            [Required]
            [MaxLength(15)]
            public string MobileNumber { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            //Navigation
            public ICollection<CompanyUser> CompanyUsers { get; set; }
        }
}
