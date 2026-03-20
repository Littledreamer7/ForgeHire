using System.Text.RegularExpressions;

namespace ForgeHire.Helpers
{
    public static class PhoneHelper
    {
        private static readonly Regex IndianMobileRegex =
            new Regex(@"^[6-9]\d{9}$", RegexOptions.Compiled);

        // ======================
        // VALIDATE MOBILE
        // ======================
        public static bool IsValidIndianMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                return false;

            mobile = NormalizeMobile(mobile);

            var core = mobile.StartsWith("+91")
                ? mobile.Substring(3)
                : mobile;

            return IndianMobileRegex.IsMatch(core);
        }

        // ======================
        // NORMALIZE MOBILE (+91 FORMAT)
        // ======================
        public static string NormalizeMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                return mobile;

            mobile = mobile.Trim();

            // remove spaces and dashes
            mobile = mobile.Replace(" ", "").Replace("-", "");

            // already +91 → keep
            if (mobile.StartsWith("+91"))
                return mobile;

            // 10 digit → convert
            if (mobile.Length == 10)
                return "+91" + mobile;

            return mobile;
        }
    }
}