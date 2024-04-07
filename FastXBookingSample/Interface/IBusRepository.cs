using FastXBookingSample.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace FastXBookingSample.Interface
{
    public interface IBusRepository
    {
        string CreateBus(Bus bus);
        string DeleteBus(int id);
        string UpdateBus(int id, Bus bus);
        List<Bus> GetAll();
        Bus GetBusById(int id);
        List<Bus> GetBusByDetails(string origin, string destination, DateOnly date);
        bool BusExists(int id);
        bool RoleExists(int id);

        string AddBusAmenity(int busid, int amenityid);
        string PatchBus(int id, JsonPatchDocument<Bus> patchBus);
    }
}
