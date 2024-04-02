using FastXBookingSample.Models;
using System;

namespace FastXBookingSample.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingContext _context;
        public BookingRepository(BookingContext context)
        {
            _context = context;
        }
        public string DeleteBooking(int id)
        {
            if (!IsBookingExists(id))
                return "Invalid Id";
            var booking = _context.Bookings.FirstOrDefault(x=>x.BookingId == id);
            _context.Bookings.Remove(booking);
            return _context.SaveChanges()>0?"Deleted Successfully":"Deletion Failed";
        }

        public List<Booking> GetAllBookingsByBusId(int busId)
        {
            return _context.Bookings.Where(x=>busId== x.BusId).ToList();
        }

        public List<Booking> GetAllBookingsByUserId(int userId)
        {
            return _context.Bookings.Where(x => userId == x.UserId).ToList();
        }

        public bool IsBookingExists(int id)
        {
            return _context.Bookings.Any(x=>x.BookingId == id);
        }

        public Booking PostBooking(Booking booking)
        {
            //if (!IsUser(Convert.ToInt32(booking.UserId)))
                //return "Not a User";
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return booking;

        }

        public bool IsUser(int userId)
        {
            return _context.Users.FirstOrDefault(x=>x.UserId == userId).Role=="User"?true:false;
        }
    }
}
