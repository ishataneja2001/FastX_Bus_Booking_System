namespace FastXBookingSample.Repository
{
    public interface IRouteRepository
    {
        List<Models.Route> GetRoutesByBusId(int busid);
        string PostBusRoute(Models.Route route);
        string UpdateBusRoute(int id, Models.Route route); 
        string DeleteBusRoute(int id);
        bool IsRouteExists(int id);
    }
}
