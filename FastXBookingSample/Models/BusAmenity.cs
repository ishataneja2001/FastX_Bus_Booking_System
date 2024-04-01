using System;
using System.Collections.Generic;

namespace FastXBookingSample.Models
{
    public partial class BusAmenity
    {
        public int Id { get; set; }
        public int? BusId { get; set; }
        public int? AmenityId { get; set; }

        public virtual Amenity? Amenity { get; set; }
        public virtual Bus? Bus { get; set; }
    }
}
