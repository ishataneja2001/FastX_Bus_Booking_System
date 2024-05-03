using System;
using System.Collections.Generic;

namespace FastXBookingSample.Models
{
    public partial class Route
    {
        public int RouteId { get; set; }
        public string PlaceName { get; set; } = null!;
        public int? BusId { get; set; }

        public virtual Bus? Bus { get; set; }
    }
}
