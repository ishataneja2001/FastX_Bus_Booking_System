using FastXBookingSample.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Drawing;

namespace FastXBookingSample.Interface
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        string PostUser(User user);
        string ModifyUserDetails(int id, User user);
        string DeleteUser(int id);
        bool IsUserExists(int id);

        string PatchUser(int id, JsonPatchDocument<User> patchuser);
    }
}
