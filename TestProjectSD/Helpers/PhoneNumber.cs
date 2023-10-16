using System.Text.RegularExpressions;
using TestProjectSD_withDatabase.Exceptions;

namespace TestProjectSD_withDatabase.Helpers
{
    public static class PhoneNumber
    {
        // Regular expression used to validate a phone number.( 0123456789, 012-345-6789, and (012)-345-6789)
        public const string motif = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";

        public static bool IsPhoneNbr(string number)
        {
            if (number != null) return Regex.IsMatch(number, motif);
            else return false;
        }

        public static string VerifyPhoneNumber(string number)
        {
            if (number != null)
            {
                if (!IsPhoneNbr(number))
                {
                    throw new IncorrectPhoneNumberException();
                }
            }
            return number ?? "";
        }
    }
}
