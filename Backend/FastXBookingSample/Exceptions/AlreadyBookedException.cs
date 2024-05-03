namespace FastXBookingSample.Exceptions
{
    public class AlreadyBookedException:Exception
    {
        public AlreadyBookedException():base("Seat already Booked") { }
        public AlreadyBookedException(string message) : base(message) { }
    }
}
