using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public interface IBusRepository
    {
        string CreateBus(Bus bus);
        string DeleteBus(int id);
        string UpdateBus(int id, Bus bus);
        List<Bus> GetAll();
        Task<Bus> GetBusById(int id);
        List<Bus> GetBusByDetails(string origin, string destination, DateOnly date);
        bool BusExists(int id);
        bool RoleExists(int id);

        string AddBusAmenity(int busid, int amenityid);
    }
}
