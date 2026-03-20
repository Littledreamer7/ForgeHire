namespace ForgeHire.Dtos.Company_Dtos
{
    namespace ForgeHire.Dtos.Company
    {
        public class CompanyProfileDto
        {
            public int Id { get; set; }

            public string CompanyName { get; set; }

            public string MobileNumber { get; set; }

            public string? GstNumber { get; set; }

            public string? CinNumber { get; set; }

            public string? Email { get; set; }

            public string Location { get; set; }

            public string IndustryType { get; set; }

            public string? Website { get; set; }

            public string? CompanySize { get; set; }

            public string? LogoUrl { get; set; }

            public bool IsVerified { get; set; }
        }
    }
}
