namespace FastXBookingSample.DTO
{
    public class BusSeatDto
    {
        public int SeatId { get; set; }
        public int? BusId { get; set; }
        public int? SeatNo { get; set; }
        public bool? IsBooked { get; set; }
    }
}
