namespace FastXBookingSample.Exceptions
{
    public class InvalidUserException: Exception
    {
        public InvalidUserException(): base("Invalid Email OR Password") { }
        public InvalidUserException(string message): base(message) { }
    }
}
