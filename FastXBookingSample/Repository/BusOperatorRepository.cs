using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public class BusOperatorRepository : IBusOperatorRepository
    {
        private readonly BookingContext _context;
        public BusOperatorRepository(BookingContext context)
        {
            _context = context;
        }
        public string DeleteBusOperator(int id)
        {
            if (!IsOperatorExists(id))
                return "Invalid Id";
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            _context.Users.Remove(user);
            return _context.SaveChanges() > 0 ? "Deleted Successfuly" : "Deletion Failed";
        }

        public List<User> GetAllBusOperators()
        {
            return _context.Users.Where(x => x.Role == "Bus Operator").ToList();
        }

        public bool IsOperatorExists(int id)
        {
            return _context.Users.Any(x => x.UserId == id && x.Role == "Bus Operator");
        }

        public string ModifyBusOperatorDetails(int id, User user)
        {
            if (!IsOperatorExists(id))
                return "Invalid Id";
            _context.Users.Update(user);

            return _context.SaveChanges() > 0 ? "Updated Succesfully" : "Updation Failed";
        }

        public string PostBusOperator(User user)
        {
            _context.Users.Add(user);

            return _context.SaveChanges() > 0 ? "Added Succesfully" : "Addition Failed";
        }
    }
}
