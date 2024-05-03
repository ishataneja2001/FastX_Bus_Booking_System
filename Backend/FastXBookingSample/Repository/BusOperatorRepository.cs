using FastXBookingSample.Models;
using FastXBookingSample.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using System.Text.RegularExpressions;
using FastXBookingSample.Interface;

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
                throw new BusOperatorNotFoundException();
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);
            _context.Users.Remove(user);
            return _context.SaveChanges() > 0 ? "Deleted Successfuly" : "Deletion Failed";
            
        }

        public List<User> GetAllBusOperators()
        {
            return _context.Users.Where(x => x.Role == "Bus Operator").ToList();
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

        public bool IsOperatorExists(int id)
        {
            return _context.Users.Any(x => x.UserId == id && x.Role == "Bus Operator");
            
        }

        public string ModifyBusOperatorDetails(int id, User user)
        {
            if (!IsEmailValid(user.Email))
                throw new InvalidUsersEmailException();
            if (!IsPasswordValid(user.Password))
                throw new InvalidUsersPasswordException();
            if (!IsOperatorExists(id))
                throw new BusOperatorNotFoundException();
            _context.Users.Update(user);

            return _context.SaveChanges() > 0 ? "Updated Succesfully" : "Updation Failed";
            
        }

        public string PatchBusOperator(int id, JsonPatchDocument<User> patchBusOperator)
        {
            if (!IsOperatorExists(id))
                throw new BusOperatorNotFoundException();
            var busOperator = _context.Users.FirstOrDefault(x => x.UserId == id);
            patchBusOperator.ApplyTo(busOperator);
            _context.Update(busOperator);

            return _context.SaveChanges() > 0? "Updated Successfully": "Updation failed" ;
            
        }

        public string PostBusOperator(User user)
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
