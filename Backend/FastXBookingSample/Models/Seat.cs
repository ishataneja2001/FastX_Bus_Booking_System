using System;
using System.Collections.Generic;

namespace FastXBookingSample.Models
{
    public partial class Seat
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

        public virtual Booking? Booking { get; set; }
    }
}
