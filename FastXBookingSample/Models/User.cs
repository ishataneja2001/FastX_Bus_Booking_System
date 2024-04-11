using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FastXBookingSample.Models
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
            Buses = new HashSet<Bus>();
        }

        public int UserId { get; set; }
        public string? Role { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string ContactNo { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Bus> Buses { get; set; }
    }
}
