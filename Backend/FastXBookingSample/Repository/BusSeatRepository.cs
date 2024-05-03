using FastXBookingSample.Exceptions;
using FastXBookingSample.Interface;
using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public class BusSeatRepository : IBusSeatRepository
    {
        private readonly BookingContext _context;
        private readonly IBusRepository _busRepository;

        public BusSeatRepository(BookingContext context, IBusRepository busRepository)
        {
            _context = context;
            _busRepository = busRepository;
        }

        public void AddSeatByBusId(int busid,int seats)
        {
            for (int i = 1; i <= seats; i++) {
                _context.BusSeats.Add(new BusSeat()
                {
                    BusId = busid,
                    SeatNo = i,
                    IsBooked = false,
                });
            }
            _context.SaveChanges();
        }

        public void DeleteSeatsByBusId(int busid)
        {
            var seats = _context.BusSeats.Where(x => x.BusId == busid).ToList();
            foreach (var seat in seats)
                _context.BusSeats.Remove(seat);
            _context.SaveChanges() ;
        }

        public List<BusSeat> GetSeatsByBusId(int busid)
        {
            if (!_busRepository.BusExists(busid))
                throw new BusNotFoundException();
            return _context.BusSeats.Where(x=>x.BusId == busid).ToList();
        }
    }
}
