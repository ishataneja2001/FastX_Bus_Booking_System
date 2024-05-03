using FastXBookingSample.Models;
using FastXBookingSample.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using System.Reflection.Metadata.Ecma335;
using FastXBookingSample.Interface;

namespace FastXBookingSample.Repository
{
    public class AmenityRepository : IAmenityRepository
    {
        private readonly BookingContext _context;
        private readonly IBusRepository _busRepository;

        public AmenityRepository(BookingContext context,IBusRepository busRepository)
        {
            _context = context;
            _busRepository = busRepository;
        }
        public string DeleteAmenity(int id)
        {
            if (!IsAmenityExists(id))
                throw new AmenityNotFoundException();
            var amenity = _context.Amenities.FirstOrDefault(a => a.AmenityId == id);
            _context.Amenities.Remove(amenity);
            return _context.SaveChanges()>0?"Deleted Successfully":"Deletion Failed";
            
        }

        public List<Amenity> GetAllAmenities()
        {
            return _context.Amenities.ToList();
            
        }

        public List<Amenity> GetAllAmenitiesByBusId(int id)
        {
            if(!_busRepository.BusExists(id))
                throw new BusNotFoundException();
            return _context.Amenities.Where(x => _context.BusAmenities.Any(ba => ba.BusId == id && ba.AmenityId == x.AmenityId)).ToList();
            
        }

        public bool IsAmenityExists(int id)
        {
            return _context.Amenities.Any(x => x.AmenityId == id);
        }

        public string PatchAmenity(int id, JsonPatchDocument<Amenity> amenityPatch)
        {
            if (!IsAmenityExists(id))
                throw new AmenityNotFoundException();
            var admentity = _context.Amenities.FirstOrDefault(x=>x.AmenityId==id);
            amenityPatch.ApplyTo(admentity);
            _context.Update(admentity);

            return _context.SaveChanges() > 0 ? "Updated Successfully" : "Updation Failed";
            
        }

        public string PostAmenity(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            return _context.SaveChanges() > 0 ? "Added Successfully" : "Addition Failed";

        }

        public string UpdateAmenity(int id, Amenity amenity)
        {
            if(!IsAmenityExists(id))
                throw new AmenityNotFoundException();
            _context.Amenities.Update(amenity);
            return _context.SaveChanges() > 0 ? "Updated Successfully" : "Updation Failed";
           

        }
    }
}
