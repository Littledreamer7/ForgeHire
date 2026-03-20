namespace ForgeHire.Auth.Auth_Dtos
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; } // internal use
        public string Role { get; set; }
        public int UserId { get; set; }
    }
}