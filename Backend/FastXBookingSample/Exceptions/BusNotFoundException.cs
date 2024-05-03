namespace FastXBookingSample.Exceptions
{
    public class BusNotFoundException:Exception
    {
        public BusNotFoundException(): base("Bus not found") { }
        public BusNotFoundException(string message) : base(message) { }
    }
}
