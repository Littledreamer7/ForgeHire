namespace ForgeHire.Models
{
    public class OtpBlock
    {
        public int Id { get; set; }

        public string MobileNumber { get; set; }

        public DateTime BlockedUntil { get; set; }

        public string Reason { get; set; }
    }
}
