namespace FastXBookingSample.Exceptions
{
    public class NoSeatsAvailableException:Exception
    {
        public NoSeatsAvailableException():base("All seats are booked") { }
        public NoSeatsAvailableException(string message) : base(message) { }
    }
}
