using System.ComponentModel.DataAnnotations;

namespace ForgeHire.Models
{
    public class OtpRecord
    {
    
            public int Id { get; set; }

            public string MobileNumber { get; set; }

            public string OtpHash { get; set; }

            public DateTime ExpiryTime { get; set; }

            public int AttemptCount { get; set; } = 0;

            public bool IsUsed { get; set; } = false;

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        }
    }
