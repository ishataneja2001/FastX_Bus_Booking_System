using FastXBookingSample.Models;

namespace FastXBookingSample.DTO
{
    public class BusDto
    {
        public int BusId { get; set; }
        public string BusName { get; set; } = null!;
        public string BusNumber { get; set; } = null!;
        public string BusType { get; set; } = null!;
        public int NoOfSeats { get; set; }
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Fare { get; set; }
        public int BusOperator { get; set; }
        public DateTime DepartureDate { get; set; }
   
    }
}
