using FastXBookingSample.Models;
using FastXBookingSample.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using FastXBookingSample.Interface;

namespace FastXBookingSample.Repository
{
    public class DroppingPointRepository : IDroppingPointRepository
    {
        private readonly BookingContext _context;
        private readonly IBusRepository _busRepository;

        public DroppingPointRepository(BookingContext context, IBusRepository busRepository)
        {
            _context = context;
            _busRepository = busRepository;
        }
        public string DeleteDroppingPoints(int id)
        {
            if (!DroppingPointExists(id))
                throw new DroppingPointNotFoundException();
            var dp=_context.DroppingPoints.FirstOrDefault(x=>x.DroppingId==id);
            _context.DroppingPoints.Remove(dp);
            return _context.SaveChanges()>0?"Deleted Successfullty":"Deletion Failed";

            
        }

        public bool DroppingPointExists(int id)
        {
            return _context.DroppingPoints.Any(x=>x.DroppingId == id);
        }

        public List<DroppingPoint> GetDroppingPointsByBusId(int busid)
        {
            if(!_busRepository.BusExists(busid))
                throw new BusNotFoundException();
            return _context.DroppingPoints.Where(x=>x.BusId==busid).ToList();
            
        }

        public string PatchDroppingPoint(int id, JsonPatchDocument<DroppingPoint> patchDroppingPoint)
        {
            if (!DroppingPointExists(id))
                throw new DroppingPointNotFoundException();
            var droppingPoint = _context.DroppingPoints.FirstOrDefault(x => x.DroppingId == id);
            patchDroppingPoint.ApplyTo(droppingPoint);
            _context.Update(droppingPoint);

            return _context.SaveChanges() > 0 ? "Updated Successfully" : "Updation Failed";

            throw new DroppingPointNotFoundException();
        }

        public string PostDroppingPoint(DroppingPoint droppingPoint)
        {
            _context.DroppingPoints.Add(droppingPoint);
            return _context.SaveChanges() > 0 ? "Added Successfullty" : "Addition Failed";
        }

        public string UpdateDroppingPoints(int id, DroppingPoint droppingPoint)
        {
            if (!DroppingPointExists(id))
                throw new DroppingPointNotFoundException();
            _context.DroppingPoints.Update(droppingPoint);
            return _context.SaveChanges() > 0 ? "Updated Successfullty" : "Updation Failed";

            
        }
    }
}
