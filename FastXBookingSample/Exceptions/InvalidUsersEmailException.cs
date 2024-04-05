namespace FastXBookingSample.Exceptions
{
    public class InvalidUsersEmailException:Exception
    {
        public InvalidUsersEmailException() : base("Invalid Email Format.") { }
        public InvalidUsersEmailException(string message) : base(message) { }
    }
}
