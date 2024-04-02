using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public interface ISeatRepository
    {
        List<Seat> GetSeatsByBookingId(int bookingId);
        List<Seat> GetSeatsByUserId(int userId);
        void PostSeatByBookingId(Seat seat);
        void DeleteSeatByBookingId(int bookindId);
        bool IsBookingExists(int id);

    }
}
