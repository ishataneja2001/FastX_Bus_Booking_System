using FastXBookingSample.Interface;
using FastXBookingSample.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace FastXBookingSample.Repository
{
    public class SeatRepository : ISeatRepository
    {
        private readonly BookingContext _context;
        private readonly IBookingHistoryRepository _historyRepository;
        public SeatRepository(BookingContext context, IBookingHistoryRepository historyRepository)
        {
            _context = context;
            _historyRepository = historyRepository;
        }
        public void DeleteSeatBySeatId(int seatId)
        {
            Seat seat = _context.Seats.FirstOrDefault(x => x.SeatId == seatId);
            BusSeat busseat = _context.BusSeats.FirstOrDefault(x => x.SeatNo == seat.SeatNumber);
            busseat.IsBooked = false;

            BookingHistory bookingHistory = _context.BookingHistories.FirstOrDefault(x => x.BookingId == seat.BookingId && x.Seats==Convert.ToString(seat.SeatNumber));
            bookingHistory.IsCancelled = true;
            _context.BookingHistories.Update(bookingHistory);

            _context.Seats.Remove(seat);
            
            _context.SaveChanges();
        }

        public List<Seat> GetSeatsByBookingId(int bookingId)
        {
            return _context.Seats.Where(x=>x.BookingId==bookingId).ToList();
        }

        public List<Seat> GetSeatsByUserId(int userId)
        {
            return _context.Seats
            .Include(s => s.Booking)
            .Where(s => s.Booking.UserId == userId)
            .ToList();
        }

        public bool IsBookingExists(int id)
        {
            return _context.Seats.Any(x=>x.BookingId == id);
        }

        public void PostSeatByBookingId(Seat seat)
        {
            seat.Amount = _context.Buses.FirstOrDefault(x=>x.BusId==(_context.Bookings.FirstOrDefault(x=>x.BookingId==seat.BookingId).BusId)).Fare;
            
            _context.Seats.Add(seat);
            BusSeat busseat = _context.BusSeats.FirstOrDefault(x => x.SeatNo == seat.SeatNumber);
            //if(busseat != null) 
            //    IRaiseEventOperation eroor
            Booking booking = _context.Bookings.FirstOrDefault(x => x.BookingId == seat.BookingId);
            User user = _context.Users.FirstOrDefault(x => x.UserId == booking.UserId);
            Bus bus = _context.Buses.FirstOrDefault(x => x.BusId == booking.BusId);
            _historyRepository.PostBookingHistory(new BookingHistory()
            {
                BookingId = seat.BookingId,
                UserName = user.Email,
                BusName = bus.BusName,
                Amount = seat.Amount,
                BusNumber = bus.BusNumber,
                Seats = Convert.ToString(seat.SeatNumber),
                IsCancelled = false,
                BookingDateTime = seat.BookingDateTime,
                PassengerName = seat.PassengerName,
                Age = seat.Age,
                Gender = seat.Gender,
            }) ;
            busseat.IsBooked = true;
            _context.BusSeats.Update(busseat);
            _context.SaveChanges();
        }
    }
}
