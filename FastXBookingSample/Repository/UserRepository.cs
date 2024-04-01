using FastXBookingSample.Models;

namespace FastXBookingSample.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BookingContext _context;

        public UserRepository(BookingContext context)
        {
            _context = context;
        }
        public string DeleteUser(int id)
        {
            if (!IsUserExists(id))
                return "Invalid Id";
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            _context.Users.Remove(user);
            return _context.SaveChanges()>0?"Deleted Successfuly":"Deleteion Failed";

        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Where(x=>x.Role=="User").ToList();
        }

        public string ModifyUserDetails(int id, User user)
        {
            if (!IsUserExists(id))
                return "Invalid Id";
            _context.Users.Update(user);

            return _context.SaveChanges() > 0 ? "Updated Succesfully" : "Updation Failed";
        }

        public string PostUser(User user)
        {
            _context.Users.Add(user);

            return _context.SaveChanges() > 0 ? "Added Succesfully" : "Addition Failed";
        }

        public bool IsUserExists(int id)
        {
            return _context.Users.Any(x=>x.UserId == id&&x.Role=="User");
        }
    }
}
