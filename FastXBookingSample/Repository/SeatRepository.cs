using FastXBookingSample.Models;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace FastXBookingSample.Repository
{
    public class SeatRepository : ISeatRepository
    {
        private readonly BookingContext _context;
        public SeatRepository(BookingContext context)
        {
            _context = context;
        }
        public void DeleteSeatByBookingId(int bookindId)
        {
            var seats = GetSeatsByBookingId(bookindId);
            foreach (var seat in seats)
            {
                BusSeat busseat = _context.BusSeats.FirstOrDefault(x => x.SeatNo == seat.SeatNumber);
                busseat.IsBooked = false;
                _context.Seats.Remove(seat);
            }
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
            busseat.IsBooked = true;
            _context.BusSeats.Update(busseat);
            _context.SaveChanges();
        }
    }
}
