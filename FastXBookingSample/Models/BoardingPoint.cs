using System;
using System.Collections.Generic;

namespace FastXBookingSample.Models
{
    public partial class BoardingPoint
    {
        public BoardingPoint()
        {
            Bookings = new HashSet<Booking>();
        }

        public int BoardingId { get; set; }
        public string? PlaceName { get; set; }
        public TimeSpan Timings { get; set; }
        public int? BusId { get; set; }

        public virtual Bus? Bus { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
