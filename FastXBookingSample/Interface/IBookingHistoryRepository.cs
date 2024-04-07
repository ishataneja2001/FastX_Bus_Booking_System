using FastXBookingSample.Models;

namespace FastXBookingSample.Interface
{
    public interface IBookingHistoryRepository
    {
        List<BookingHistory> GetAll();
        void PostBookingHistory(BookingHistory bookingHistory);
    }
}
