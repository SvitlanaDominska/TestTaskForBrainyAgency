namespace TestProjectSD_withDatabase.Exceptions
{
    public class IncorrectEmailAddressException:Exception
    {       
        private static string errorMessage = "Email Address is incorrect. Please add correct Email Address.";
        public static string ErrorMessage { get => errorMessage; set => errorMessage = value; }

        public IncorrectEmailAddressException() : base(ErrorMessage) { }
    }
}
