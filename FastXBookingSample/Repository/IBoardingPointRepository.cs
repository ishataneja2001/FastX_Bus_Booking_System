using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public interface IBoardingPointRepository
    {
        List<BoardingPoint> GetBoardingPointsByBusId(int busid);
        string DeleteBoardingPoints(int id);
        string UpdateBoardingPoints(int id,BoardingPoint boardingPoint);
        string PostBoardingPoint(BoardingPoint boardingPoint);
        bool BoardingPointExists(int id);
    }
}
