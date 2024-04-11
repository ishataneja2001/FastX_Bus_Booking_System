using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public interface IBusSeatRepository
    {
        List<BusSeat> GetSeatsByBusId(int busid);
        void AddSeatByBusId(int busid,int seats);
        void DeleteSeatsByBusId(int busid);
    }
}
