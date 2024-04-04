namespace FastXBookingSample.DTO
{
    public class SeatDto
    {
        public int SeatId { get; set; }
        public int SeatNumber { get; set; }
        public int? BookingId { get; set; }
        public int Amount { get; set; }
        public string CardDetails { get; set; } = null!;
        public DateTime BookingDateTime { get; set; }
        public string PassengerName { get; set; } = null!;
        public string? Gender { get; set; }
        public int Age { get; set; }
    }
}
