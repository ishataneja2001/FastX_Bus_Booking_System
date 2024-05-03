using System;
using System.Collections.Generic;

namespace FastXBookingSample.Models
{
    public partial class Amenity
    {
        public Amenity()
        {
            BusAmenities = new HashSet<BusAmenity>();
        }

        public int AmenityId { get; set; }
        public string? AmenityName { get; set; }

        public virtual ICollection<BusAmenity> BusAmenities { get; set; }
    }
}
