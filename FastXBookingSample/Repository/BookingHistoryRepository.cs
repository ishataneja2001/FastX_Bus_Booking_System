using FastXBookingSample.Interface;
using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public class BookingHistoryRepository : IBookingHistoryRepository
    {
        private readonly BookingContext _context;
        public BookingHistoryRepository(BookingContext context)
        {
            _context = context;
        }

        public List<BookingHistory> GetAll()
        {
            return _context.BookingHistories.ToList();
        }

        public void PostBookingHistory(BookingHistory bookingHistory)
        {
            _context.BookingHistories.Add(bookingHistory);
        }
    }
}
