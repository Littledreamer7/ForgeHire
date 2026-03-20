namespace ForgeHire.Models
{
    public class CompanyProfile
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string CompanyName { get; set; }

        public string Industry { get; set; }

        public string Location { get; set; }

        public string ContactEmail { get; set; }

        public User User { get; set; }
    }
}
