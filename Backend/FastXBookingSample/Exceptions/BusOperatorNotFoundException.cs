namespace FastXBookingSample.Exceptions
{
    public class BusOperatorNotFoundException:Exception
    {
        public BusOperatorNotFoundException():base("BusOperator not found") { }
        public BusOperatorNotFoundException(string message):base(message) { }
    }
}
