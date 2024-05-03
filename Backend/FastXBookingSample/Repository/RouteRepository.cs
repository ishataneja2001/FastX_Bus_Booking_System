using FastXBookingSample.Models;
using FastXBookingSample.Exceptions;

using Microsoft.AspNetCore.JsonPatch;
using FastXBookingSample.Interface;

namespace FastXBookingSample.Repository
{
    public class RouteRepository : IRouteRepository
    {
        private readonly BookingContext _context;
        private readonly IBusRepository _busRepository;

        public RouteRepository(BookingContext context, IBusRepository busRepository)
        {
            _context = context;
            _busRepository = busRepository;
        }
        public List<Models.Route> GetRoutesByBusId(int busid)
        {
            if(!_busRepository.BusExists(busid))
                throw new BusNotFoundException();
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
                throw new RouteNotFoundException();
            _context.Routes.Update(route);
            return _context.SaveChanges() > 0 ? "Updated Successfully" : "Updation Failed";

            
        }

        public string DeleteBusRoute(int id)
        {
            if (!IsRouteExists(id))
                throw new RouteNotFoundException();
            var route = _context.Routes.FirstOrDefault(x=>x.RouteId == id);
            _context.Routes.Remove(route);
            return _context.SaveChanges() > 0 ? "Deleted Successfully" : "Deletion Failed";

            

        }




        public bool IsRouteExists(int id)
        {
            return _context.Routes.Any(x=>x.RouteId == id);
        }

        public string PatchRoute(int id, JsonPatchDocument<Models.Route> patchRoute)
        {
            if (!IsRouteExists(id))
                return "InValid Id";
            var route = _context.Routes.FirstOrDefault(y => y.RouteId == id);
            patchRoute.ApplyTo(route);
            _context.Update(route);

            return _context.SaveChanges() > 0 ? "Updated Successfully" : "Updation Failed";
        }
    }
}
