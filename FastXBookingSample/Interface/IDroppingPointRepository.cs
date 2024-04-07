using FastXBookingSample.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace FastXBookingSample.Interface
{
    public interface IDroppingPointRepository
    {
        List<DroppingPoint> GetDroppingPointsByBusId(int busid);
        string DeleteDroppingPoints(int id);
        string UpdateDroppingPoints(int id, DroppingPoint droppingPoint);
        string PostDroppingPoint(DroppingPoint droppingPoint);
        bool DroppingPointExists(int id);

        string PatchDroppingPoint(int id, JsonPatchDocument<DroppingPoint> patchDroppingPoint);
    }
}
