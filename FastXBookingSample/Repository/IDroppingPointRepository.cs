using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public interface IDroppingPointRepository
    {
        List<DroppingPoint> GetDroppingPointsByBusId(int busid);
        string DeleteDroppingPoints(int id);
        string UpdateDroppingPoints(int id, DroppingPoint droppingPoint);
        string PostDroppingPoint(DroppingPoint droppingPoint);
        bool DroppingPointExists(int id);
    }
}
