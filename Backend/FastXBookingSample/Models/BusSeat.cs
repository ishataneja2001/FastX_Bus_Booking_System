using System;
using System.Collections.Generic;

namespace FastXBookingSample.Models
{
    public partial class BusSeat
    {
        public int SeatId { get; set; }
        public int? BusId { get; set; }
        public int? SeatNo { get; set; }
        public bool? IsBooked { get; set; }

        public virtual Bus? Bus { get; set; }
    }
}
