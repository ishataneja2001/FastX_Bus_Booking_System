using FastXBookingSample.Models;

namespace FastXBookingSample.Interface
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBookingsByBusId(int busId);
        List<Booking> GetAllBookingsByUserId(int userId);
        Booking PostBooking(Booking booking);
        string DeleteBooking(int id);
        bool IsBookingExists(int id);
    }
}
