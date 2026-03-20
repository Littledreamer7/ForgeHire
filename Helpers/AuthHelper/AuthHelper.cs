namespace ForgeHire.Helpers.AuthHelper
{
    public static class AuthHelper
    {
        public static void EnsureAdminOrHR(string role)
        {
            if (role != "Admin" && role != "HR")
                throw new UnauthorizedAccessException("Permission denied");
        }

        public static void EnsureAdmin(string role)
        {
            if (role != "Admin")
                throw new UnauthorizedAccessException("Only Admin allowed");
        }
    }
}
