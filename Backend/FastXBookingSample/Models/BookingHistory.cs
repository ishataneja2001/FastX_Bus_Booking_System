using System;
using System.Collections.Generic;

namespace FastXBookingSample.Models
{
    public partial class BookingHistory
    {
        public int BookId { get; set; }
        public int? BookingId { get; set; }
        public string? UserName { get; set; }
        public int Amount { get; set; }
        public string? BusName { get; set; }
        public string BusNumber { get; set; } = null!;
        public string Seats { get; set; } = null!;
        public bool IsCancelled { get; set; }
        public DateTime BookingDateTime { get; set; }
        public string PassengerName { get; set; } = null!;
        public string? Gender { get; set; }
        public int Age { get; set; }

        public virtual Booking? Booking { get; set; }
    }
}
