using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public class DroppingPointRepository : IDroppingPointRepository
    {
        private readonly BookingContext _context;

        public DroppingPointRepository(BookingContext context)
        {
            _context = context;
        }
        public string DeleteDroppingPoints(int id)
        {
            if (!DroppingPointExists(id))
                return "Invalid Id";
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
            return _context.DroppingPoints.Where(x=>x.BusId==busid).ToList();
        }

        public string PostDroppingPoint(DroppingPoint droppingPoint)
        {
            _context.DroppingPoints.Add(droppingPoint);
            return _context.SaveChanges() > 0 ? "Added Successfullty" : "Addition Failed";
        }

        public string UpdateDroppingPoints(int id, DroppingPoint droppingPoint)
        {
            if (!DroppingPointExists(id))
                return "Invalid Id";
            _context.DroppingPoints.Update(droppingPoint);
            return _context.SaveChanges() > 0 ? "Updated Successfullty" : "Updation Failed";
        }
    }
}
