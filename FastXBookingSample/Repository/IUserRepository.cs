using FastXBookingSample.Models;
using System.Drawing;

namespace FastXBookingSample.Repository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        string PostUser(User user);
        string ModifyUserDetails(int id, User user);
        string DeleteUser(int id);
        bool IsUserExists(int id);
    }
}
