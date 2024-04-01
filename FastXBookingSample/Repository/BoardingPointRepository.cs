using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public class BoardingPointRepository:IBoardingPointRepository
    {
        private readonly BookingContext _context;
        public BoardingPointRepository(BookingContext context)
        {
            _context = context;
        }

        public bool BoardingPointExists(int id)
        {
            return _context.BoardingPoints.Any(x => x.BoardingId ==id);
        }

        public string DeleteBoardingPoints(int id)
        {
            if (!BoardingPointExists(id))
                return "Invalid id";
            var bp = _context.BoardingPoints.FirstOrDefault(x => x.BoardingId == id);
            _context.BoardingPoints.Remove(bp);
            int s = _context.SaveChanges();
            return s > 0 ? "Succesfully Deleted" : "Deletion Failed";
        }


        public List<BoardingPoint> GetBoardingPointsByBusId(int busid)
        {
            return _context.BoardingPoints.Where(x => x.BusId == busid).ToList();
        }

        public string PostBoardingPoint(BoardingPoint boardingPoint)
        {
            _context.BoardingPoints.Add(boardingPoint);
            int s = _context.SaveChanges();
            return s > 0 ? "Succesfully Added" : "Additon Failed";
        }

        public string UpdateBoardingPoints(int id, BoardingPoint boardingPoint)
        {
            if (!BoardingPointExists(id)) return "Updation Failed";
            _context.BoardingPoints.Update(boardingPoint);
            int s = _context.SaveChanges();
            return s > 0 ? "Succesfully Updated" : "Updation Failed";
        }
    }
}
