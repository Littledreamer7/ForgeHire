namespace ForgeHire.Dtos.Company_Dtos
{
    public class UpdateCompanyDto
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string? GstNumber { get; set; }

        public string? CinNumber { get; set; }

        public string? Email { get; set; }

        public string Location { get; set; }

        public string IndustryType { get; set; }

        public string? Website { get; set; }

        public string? CompanySize { get; set; }
    }
}
