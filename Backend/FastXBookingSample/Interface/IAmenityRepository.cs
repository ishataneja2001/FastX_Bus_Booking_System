using FastXBookingSample.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Runtime.Intrinsics.Arm;

namespace FastXBookingSample.Interface
{
    public interface IAmenityRepository
    {
        List<Amenity> GetAllAmenities();
        List<Amenity> GetAllAmenitiesByBusId(int id);
        string PostAmenity(Amenity amenity);
        string UpdateAmenity(int id, Amenity amenity);
        string DeleteAmenity(int id);
        bool IsAmenityExists(int id);
        string PatchAmenity(int id, JsonPatchDocument<Amenity> patchEntity);
    }
}
