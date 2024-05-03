using Microsoft.AspNetCore.JsonPatch;

namespace FastXBookingSample.Interface
{
    public interface IRouteRepository
    {
        List<Models.Route> GetRoutesByBusId(int busid);
        string PostBusRoute(Models.Route route);
        string UpdateBusRoute(int id, Models.Route route);
        string DeleteBusRoute(int id);
        bool IsRouteExists(int id);

        string PatchRoute(int id, JsonPatchDocument<Models.Route> patchRoute);
    }
}
