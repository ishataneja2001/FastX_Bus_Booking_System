namespace FastXBookingSample.Exceptions
{
    public class BoardingPointNotFoundException: Exception
    {
        public BoardingPointNotFoundException() : base("Boarding point not found")
        {

        }

        public BoardingPointNotFoundException(string message) : base(message)
        {

        }
    }
}
