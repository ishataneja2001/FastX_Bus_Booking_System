namespace FastXBookingSample.DTO
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int? UserId { get; set; }
        public int? BusId { get; set; }
        
        public int? BoardingId { get; set; }
        public int? DroppingId { get; set; }
    }
}
