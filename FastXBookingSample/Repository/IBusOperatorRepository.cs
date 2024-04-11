using FastXBookingSample.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace FastXBookingSample.Repository
{
    public interface IBusOperatorRepository
    {
        List<User> GetAllBusOperators();
        string PostBusOperator(User user);
        string ModifyBusOperatorDetails(int id, User user);
        string DeleteBusOperator(int id);
        bool IsOperatorExists(int id);

        string PatchBusOperator(int id, JsonPatchDocument<User> patchBusOperator);
    }
}
