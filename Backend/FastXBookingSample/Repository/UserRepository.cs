using FastXBookingSample.Models;
using FastXBookingSample.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using System.Text.RegularExpressions;
using FastXBookingSample.Interface;

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
                throw new UserNotFoundException();
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            _context.Users.Remove(user);
            return _context.SaveChanges()>0?"Deleted Successfuly":"Deletion Failed";


        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Where(x=>x.Role=="User").ToList();
        }

        public string ModifyUserDetails(int id, User user)
        {
            if (!IsEmailValid(user.Email))
                throw new InvalidUsersEmailException();
            if (!IsPasswordValid(user.Password))
                throw new InvalidUsersPasswordException();
            if (!IsUserExists(id))
                throw new UserNotFoundException();
            _context.Users.Update(user);

            return _context.SaveChanges() > 0 ? "Updated Succesfully" : "Updation Failed";
           
        }

        public string PostUser(User user)
        {
            if (!IsEmailValid(user.Email))
                throw new InvalidUsersEmailException();
            if (!IsPasswordValid(user.Password))
                throw new InvalidUsersPasswordException();
            _context.Users.Add(user);

            return _context.SaveChanges() > 0 ? "Added Succesfully" : "Addition Failed";
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
        public bool IsUserExists(int id)
        {
            return _context.Users.Any(x=>x.UserId == id&&x.Role=="User");
        }

        public string PatchUser(int id, JsonPatchDocument<User> patchuser)
        {

            if (!IsUserExists(id)) 
                throw new UserNotFoundException();
            var patch = _context.Users.FirstOrDefault(x => x.UserId == id);
            patchuser.ApplyTo(patch);
            _context.Update(patch);

            return _context.SaveChanges() > 0 ? "Updated Sucessfully" : "Updation Failed";

            
        }
    }
}
