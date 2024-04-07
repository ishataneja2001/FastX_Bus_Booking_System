using FastXBookingSample.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace FastXBookingSample.Interface
{
    public interface IBoardingPointRepository
    {
        List<BoardingPoint> GetBoardingPointsByBusId(int busid);
        string DeleteBoardingPoints(int id);
        string UpdateBoardingPoints(int id, BoardingPoint boardingPoint);
        string PostBoardingPoint(BoardingPoint boardingPoint);
        bool BoardingPointExists(int id);
        string PatchBoardingPoint(int id, JsonPatchDocument<BoardingPoint> boardingPointPatch);
    }
}
