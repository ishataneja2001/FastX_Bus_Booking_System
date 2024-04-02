using FastXBookingSample.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FastXBookingSample.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly BookingContext _context;
        public AdminRepository(BookingContext context)
        {
            _context = context;
        }
        public string DeleteAdmin(int id)
        {
            if (!IsAdminExists(id))
                return "Invalid Id";
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            _context.Users.Remove(user);
            return _context.SaveChanges() > 0 ? "Deleted Successfuly" : "Deletion Failed";
        }

        public List<User> GetAllAdmin()
        {
            return _context.Users.Where(x => x.Role == "Admin").ToList();
        }

        public bool IsAdminExists(int id)
        {
            return _context.Users.Any(x => x.UserId == id && x.Role == "Admin");
        }

        public string ModifyAdminDetails(int id, User user)
        {
            if (!IsAdminExists(id))
                return "Invalid Id";
            _context.Users.Update(user);

            return _context.SaveChanges() > 0 ? "Updated Succesfully" : "Updation Failed";
        }

        public string PatchAdmin(int id, JsonPatchDocument<User> adminPatch)
        {
            if (!IsAdminExists(id))
                return "Invalid Id";
            var admin = _context.Users.FirstOrDefault(x=>x.UserId == id);
            adminPatch.ApplyTo(admin);
            _context.Update(admin);

            return _context.SaveChanges() > 0 ? "Updated Succesfully" : "Updation Failed";
        }

        public string PostAdmin(User user)
        {
            _context.Users.Add(user);

            return _context.SaveChanges() > 0 ? "Added Succesfully" : "Addition Failed";
        }
    }
}
