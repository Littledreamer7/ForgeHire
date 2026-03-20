namespace ForgeHire.Dtos.Job_Application_Dtos
{
    public class ApplicationResponseDto
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Status { get; set; }
        public DateTime AppliedAt { get; set; }
    }
}
