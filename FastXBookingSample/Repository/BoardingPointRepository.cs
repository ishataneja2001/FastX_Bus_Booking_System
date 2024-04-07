using FastXBookingSample.Models;
using FastXBookingSample.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using FastXBookingSample.Interface;

namespace FastXBookingSample.Repository
{
    public class BoardingPointRepository:IBoardingPointRepository
    {
        private readonly BookingContext _context;
        private readonly IBusRepository _busRepository;
        public BoardingPointRepository(BookingContext context, IBusRepository busRepository)
        {
            _context = context;
            _busRepository = busRepository;
        }

        public bool BoardingPointExists(int id)
        {
            return _context.BoardingPoints.Any(x => x.BoardingId ==id);
        }

        public string DeleteBoardingPoints(int id)
        {
            if (!BoardingPointExists(id))
                throw new BoardingPointNotFoundException();
            var bp = _context.BoardingPoints.FirstOrDefault(x => x.BoardingId == id);
            _context.BoardingPoints.Remove(bp);
            int s = _context.SaveChanges();
            return s > 0 ? "Succesfully Deleted" : "Deletion Failed";
            
        }


        public List<BoardingPoint> GetBoardingPointsByBusId(int busid)
        {
            if(!_busRepository.BusExists(busid))
                throw new BusNotFoundException();
            return _context.BoardingPoints.Where(x => x.BusId == busid).ToList();
            
        }

        public string PatchBoardingPoint(int id, JsonPatchDocument<BoardingPoint> boardingPointPatch)
        {
            if (!BoardingPointExists(id))
                throw new BoardingPointNotFoundException();
            var boardingpoint = _context.BoardingPoints.FirstOrDefault(x => x.BoardingId == id);
            boardingPointPatch.ApplyTo(boardingpoint);
            _context.Update(boardingpoint);

            return _context.SaveChanges() > 0 ? "Updated Successfully" : "Updation Failed";
        }

        public string PostBoardingPoint(BoardingPoint boardingPoint)
        {
            _context.BoardingPoints.Add(boardingPoint);
            int s = _context.SaveChanges();
            return s > 0 ? "Succesfully Added" : "Additon Failed";
        }

        public string UpdateBoardingPoints(int id, BoardingPoint boardingPoint)
        {
            if (!BoardingPointExists(id)) 
                throw new BoardingPointNotFoundException();

            _context.BoardingPoints.Update(boardingPoint);
            int s = _context.SaveChanges();
            return s > 0 ? "Succesfully Updated" : "Updation Failed";
        }
    }
}
