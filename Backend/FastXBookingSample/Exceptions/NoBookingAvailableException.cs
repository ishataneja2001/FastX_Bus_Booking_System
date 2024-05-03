using FastXBookingSample.Models;

namespace FastXBookingSample.Exceptions
{
    public class NoBookingAvailableException:Exception
    {
        public NoBookingAvailableException(): base("Booking not available") { }
        public NoBookingAvailableException(string message) : base(message) { }
    }
}
