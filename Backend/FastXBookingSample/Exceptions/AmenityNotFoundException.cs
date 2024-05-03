namespace FastXBookingSample.Exceptions
{
    public class AmenityNotFoundException : Exception
    {
        public AmenityNotFoundException() : base("Amenity not found.")
        {
        }

        public AmenityNotFoundException(string message) : base(message)
        {
        }
    }
}
