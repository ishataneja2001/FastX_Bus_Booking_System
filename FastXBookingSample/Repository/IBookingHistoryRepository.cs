using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public interface IBookingHistoryRepository
    {
        List<BookingHistory> GetAll();
        void PostBookingHistory(BookingHistory bookingHistory);
    }
}
