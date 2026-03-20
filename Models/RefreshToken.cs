namespace ForgeHire.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Role { get; set; }

        public int? CompanyId { get; set; }   // 🔥 NEW

        public string TokenHash { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}