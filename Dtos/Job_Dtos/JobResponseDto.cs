namespace ForgeHire.Dtos.Job_Dtos
{
    public class JobResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Department { get; set; }

        public string Location { get; set; }

        public string Status { get; set; }

        public int ApplicantsCount { get; set; }

        public DateTime PostedDate { get; set; }
    }
}