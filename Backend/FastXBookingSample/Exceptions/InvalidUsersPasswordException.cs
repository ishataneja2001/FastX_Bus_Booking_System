namespace FastXBookingSample.Exceptions
{
    public class InvalidUsersPasswordException:Exception
    {
        public InvalidUsersPasswordException(): base("Invalid Password Format. Password requirements:\r\n\r\nAt least one lowercase letter.\r\nAt least one uppercase letter.\r\nAt least one digit.\r\nAt least one special character that is not a digit or a letter.\r\nMinimum length of 8 characters. ") { }
        public InvalidUsersPasswordException(string message) : base(message) { }
    }
}
