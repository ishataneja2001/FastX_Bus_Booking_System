using System;
using System.Collections.Generic;

namespace FastXBookingSample.Models
{
    public partial class DroppingPoint
    {
        public int DroppingId { get; set; }
        public string PlaceName { get; set; } = null!;
        public TimeSpan Timings { get; set; }
        public int? BusId { get; set; }

        public virtual Bus? Bus { get; set; }
    }
}
