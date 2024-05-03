using FastXBookingSample.Models;

namespace FastXBookingSample.Interface
{
    public interface ISeatRepository
    {
        List<Seat> GetSeatsByBookingId(int bookingId);
        List<Seat> GetSeatsByUserId(int userId);
        string PostSeatByBookingId(Seat seat);
        void DeleteSeatBySeatId(int seatId);
        bool IsBookingExists(int id);

    }
}
