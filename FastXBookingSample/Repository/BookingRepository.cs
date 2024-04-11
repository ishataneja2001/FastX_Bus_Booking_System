using FastXBookingSample.Models;
using FastXBookingSample.Exceptions;
using System;
using FastXBookingSample.Interface;

namespace FastXBookingSample.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingContext _context;
        private readonly IBusRepository _busRepository;
        private readonly IUserRepository _userRepository;
        public BookingRepository(BookingContext context, IBusRepository busRepository, IUserRepository userRepository)
        {
            _context = context;
            _busRepository = busRepository;
            _userRepository = userRepository;
        }
        public string DeleteBooking(int id)
        {
            if (!IsBookingExists(id))
                throw new BookingNotFoundException();
            var booking = _context.Bookings.FirstOrDefault(x=>x.BookingId == id);
            _context.Bookings.Remove(booking);
            return _context.SaveChanges()>0?"Deleted Successfully":"Deletion Failed";
        }

        public List<Booking> GetAllBookingsByBusId(int busId)
        {
            if(!_busRepository.BusExists(busId))
                throw new BusNotFoundException();
            return _context.Bookings.Where(x=>busId== x.BusId).ToList();
            
        }

        public List<Booking> GetAllBookingsByUserId(int userId)
        {
            if (!_userRepository.IsUserExists(userId))
                throw new UserNotFoundException();
            return _context.Bookings.Where(x => userId == x.UserId).ToList();
            
        }

        public bool IsBookingExists(int id)
        {
            return _context.Bookings.Any(x=>x.BookingId == id);
        }

        public Booking PostBooking(Booking booking)
        {
            if (!IsUser(Convert.ToInt32(booking.UserId)))
                throw new UserNotFoundException();
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
