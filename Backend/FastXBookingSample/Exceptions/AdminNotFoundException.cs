namespace FastXBookingSample.Exceptions
{
    public class AdminNotFoundException : Exception
    {
        string message;
        public AdminNotFoundException()
        {
            message = "Admin not found";
        }
        public override string Message => message;
    }
}
