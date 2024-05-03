using System;
using System.Collections.Generic;

namespace FastXBookingSample.Models
{
    public partial class BoardingPoint
    {
        public int BoardingId { get; set; }
        public string? PlaceName { get; set; }
        public TimeSpan Timings { get; set; }
        public int? BusId { get; set; }

        public virtual Bus? Bus { get; set; }
    }
}
