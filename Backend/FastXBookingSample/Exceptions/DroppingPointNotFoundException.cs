namespace FastXBookingSample.Exceptions
{
    public class DroppingPointNotFoundException:Exception
    {
        public DroppingPointNotFoundException(): base("Dropping Point not found") { }
        public DroppingPointNotFoundException(string message) : base(message) { }
    }
}
