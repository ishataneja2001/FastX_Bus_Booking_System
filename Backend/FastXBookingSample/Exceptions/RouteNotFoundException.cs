namespace FastXBookingSample.Exceptions
{
    public class RouteNotFoundException:Exception
    {
        public RouteNotFoundException():base("Route not found") { }
        public RouteNotFoundException(string message):base(message) { }
    }
}
