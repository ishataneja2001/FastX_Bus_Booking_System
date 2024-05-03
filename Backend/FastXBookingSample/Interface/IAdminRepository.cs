using FastXBookingSample.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace FastXBookingSample.Interface
{
    public interface IAdminRepository
    {
        List<User> GetAllAdmin();
        string PostAdmin(User user);
        string ModifyAdminDetails(int id, User user);
        string DeleteAdmin(int id);
        bool IsAdminExists(int id);
        string PatchAdmin(int id, JsonPatchDocument<User> patchEntity);
    }
}
