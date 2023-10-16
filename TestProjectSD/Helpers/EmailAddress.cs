using System.Text.RegularExpressions;
using TestProjectSD_withDatabase.Exceptions;

namespace TestProjectSD_withDatabase.Helpers
{
    public class EmailAddress
    {
        // Regular expression used to validate email address( sdomins@example.com, sdomins@example.net...")
        public const string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

        public static bool IsEmailAddress(string emailAddress)
        {
            if (emailAddress != null) return Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase);
            else return false;
        }
        public static string VerifyEmailAddress(string emailAddress)
        {
            if (emailAddress != null)
            {
                if (!IsEmailAddress(emailAddress))
                {
                    throw new IncorrectEmailAddressException();
                }
            }
            return emailAddress ?? "";
        }
    }
}
