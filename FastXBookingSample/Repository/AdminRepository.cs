using FastXBookingSample.Exceptions;
using FastXBookingSample.Interface;
using FastXBookingSample.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;

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
                throw new AdminNotFoundException();
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            _context.Users.Remove(user);
            return _context.SaveChanges() > 0 ? "Deleted Successfuly" : "Deletion Failed";
            
        }

        public List<User> GetAllAdmin()
        {
            return _context.Users.Where(x => x.Role == "Admin").ToList();
            
        }

        public static bool IsEmailValid(string email)
        {          
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool IsPasswordValid(string password)
        {           
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$";
            return Regex.IsMatch(password, passwordPattern);
        }

        public bool IsAdminExists(int id)
        {
            return _context.Users.Any(x => x.UserId == id && x.Role == "Admin");
            
        }

        public string ModifyAdminDetails(int id, User user)
        {
            if (!IsEmailValid(user.Email))
                throw new InvalidUsersEmailException();
            if (!IsPasswordValid(user.Password))
                throw new InvalidUsersPasswordException();
            if (!IsAdminExists(id))
                throw new AdminNotFoundException();
            _context.Users.Update(user);

            return _context.SaveChanges() > 0 ? "Updated Succesfully" : "Updation Failed";
            
        }

        public string PatchAdmin(int id, JsonPatchDocument<User> adminPatch)
        {
            
            if (!IsAdminExists(id))
                throw new AdminNotFoundException();
            var admin = _context.Users.FirstOrDefault(x=>x.UserId == id);
            adminPatch.ApplyTo(admin);
            _context.Update(admin);

            return _context.SaveChanges() > 0 ? "Updated Succesfully" : "Updation Failed";
            
        }

        public string PostAdmin(User user)
        {
            if (!IsEmailValid(user.Email))
                throw new InvalidUsersEmailException();
            if (!IsPasswordValid(user.Password))
                throw new InvalidUsersPasswordException();
            _context.Users.Add(user);

            return _context.SaveChanges() > 0 ? "Added Succesfully" : "Addition Failed";

        }
    }
}
