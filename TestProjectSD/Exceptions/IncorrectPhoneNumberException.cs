namespace TestProjectSD_withDatabase.Exceptions
{
    public class IncorrectPhoneNumberException : Exception
    {       
        private static string errorMessage = "Phone number is incorrect. Please add correct phone number.";
        public static string ErrorMessage { get => errorMessage; set => errorMessage = value; }

        public IncorrectPhoneNumberException() : base(ErrorMessage) { }

    
      
    }
}
