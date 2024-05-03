using FastXBookingSample.Models;

namespace FastXBookingSample.Interface
{
    public interface IBookingHistoryRepository
    {
        List<BookingHistory> GetAll();
        List<BookingHistory> GetCancelledBookingsByUserId(int userId);
        List<BookingHistory> GetAllBookingsByUserId(int userId);
        List<BookingHistory> GetAllBookingsByBus(int busId);
        void PostBookingHistory(BookingHistory bookingHistory);
    }
}
