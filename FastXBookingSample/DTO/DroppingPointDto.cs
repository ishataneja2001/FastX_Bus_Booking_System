namespace FastXBookingSample.DTO
{
    public class DroppingPointDto
    {
        public int DroppingId { get; set; }
        public string PlaceName { get; set; } = null!;
        public TimeSpan Timings { get; set; }
        public int? BusId { get; set; }
    }
}
