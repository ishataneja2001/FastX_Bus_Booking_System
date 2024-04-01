namespace FastXBookingSample.DTO
{
    public class BoardingPointDto
    {
        public int BoardingId { get; set; }
        public string? PlaceName { get; set; }
        public TimeSpan Timings { get; set; }
        public int? BusId { get; set; }
    }
}
