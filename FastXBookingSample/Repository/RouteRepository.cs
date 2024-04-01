using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public class RouteRepository : IRouteRepository
    {
        private readonly BookingContext _context;

        public RouteRepository(BookingContext context)
        {
            _context = context;
        }
        public List<Models.Route> GetRoutesByBusId(int busid)
        {
            return _context.Routes.Where(x=>x.BusId == busid).ToList();
        }

        public string PostBusRoute(Models.Route route)
        {
            _context.Routes.Add(route);
            return _context.SaveChanges()>0?"Route Successfully Added":"Not Added";
        }

        public string UpdateBusRoute(int id, Models.Route route)
        {
            if (!IsRouteExists(id)) 
                return "Invalid Id";
            _context.Routes.Update(route);
            return _context.SaveChanges() > 0 ? "Updated Successfully" : "Updation Failed";
        }

        public string DeleteBusRoute(int id)
        {
            if (!IsRouteExists(id)) 
                return "Invalid Id";
            var route = _context.Routes.FirstOrDefault(x=>x.RouteId == id);
            _context.Routes.Remove(route);
            return _context.SaveChanges() > 0 ? "Deleted Successfully" : "Deletion Failed";

        }




        public bool IsRouteExists(int id)
        {
            return _context.Routes.Any(x=>x.RouteId == id);
        }

        
    }
}
