namespace FastXBookingSample.Exceptions
{
    public class NoBusAvailableException:Exception
    {
        public NoBusAvailableException(): base("No bus available for booking") { }
        public NoBusAvailableException(string message) : base(message) { }
    }
}
