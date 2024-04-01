using FastXBookingSample.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace FastXBookingSample.Repository
{
    public class BusRepository:IBusRepository
    {
        private readonly BookingContext _context;
        public BusRepository(BookingContext context)
        {
            _context = context;
        }

        public string CreateBus(Bus bus)
        {
            _context.Buses.Add(bus);
            int s=_context.SaveChanges();
            if (s > 0)
                return "Succussfully Added";
            else return "Not Added";
        }

        public string DeleteBus(int id)
        {
            if (_context.Buses == null)
                return "Bus Not Found";
            Bus bus = _context.Buses.FirstOrDefault(x=>x.BusId == id);
            if (bus == null) 
                return "Bus Not Found";
            _context.Buses.Remove(bus);
            int s = _context.SaveChanges();
            if (s > 0) 
                return "Bus Successfully Deleted";
            return "Bus Not Deleted";
        }

        public List<Bus> GetAll()
        {
            List<Bus> list = _context.Buses.ToList();
            return list;
        }

        public List<Bus> GetBusByDetails(string origin, string destination, DateOnly date)
        {
            List<Bus> buses = _context.Buses.Where(x => x.Origin == origin && x.Destination == destination && x.DepartureDate.ToShortDateString() == date.ToString())   .ToList();
            return buses;
        }

        public Bus GetBusById(int id)
        {
            Bus bus = _context.Buses.FirstOrDefault(x => x.BusId == id);
            return bus;
        }

        public string UpdateBus(int id, Bus bus)
        {
            _context.Entry(bus).State = EntityState.Modified;
            int s = _context.SaveChanges();
            if (s > 0)
                return "Bus Details Updated";
            return "Bus Details Not Updated";
        }


        public bool BusExists(int id)
        {
            return (_context.Buses?.Any(e => e.BusId == id)).GetValueOrDefault();
        }

        public bool RoleExists(int id)
        {
            return (_context.Users.Any(e => e.UserId == id && e.Role == "Bus Operator"));
        }



        public string AddBusAmenity(int busid, int amenityid)
        {
            var bus = _context.Buses.FirstOrDefault(e => e.BusId == busid);
            var amenity = _context.Amenities.FirstOrDefault(x => x.AmenityId == amenityid);
            BusAmenity busAmenity = new BusAmenity()
            {
                Bus = bus,
                AmenityId = amenityid,
            };
            _context.BusAmenities.Add(busAmenity);
            return _context.SaveChanges()>0?"Added Successfully":"Addition Failed";
        }

    }
}
