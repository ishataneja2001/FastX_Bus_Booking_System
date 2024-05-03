using FastXBookingSample.Interface;
using FastXBookingSample.Models;
using Microsoft.AspNetCore.JsonPatch;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastXBookingTest
{
    [TestFixture]
    public class AmenityRepoTest
    {
        private Mock<IAmenityRepository> _amenityRepository;

        [SetUp] 
        public void Setup()
        {
            _amenityRepository= new Mock<IAmenityRepository>();
        }

        [Test]
        public async Task TestForDeleteAmenity()
        {
            _amenityRepository.Setup(x=>x.DeleteAmenity(It.IsAny<int>())).Returns("Deleted Successfully");
            string output = await Task.Run(() => _amenityRepository.Object.DeleteAmenity(2));
            Assert.AreEqual(output, "Deleted Successfully");
        }


        [Test]
        public async Task TestForGetAllAmenities()
        {
            _amenityRepository.Setup(x => x.GetAllAmenities()).Returns(new List<Amenity>()
            {
                new Amenity()
                {
                    AmenityName="Water Bottle"
                },
                new Amenity()
                {
                    AmenityName="Charging Port"
                }
            });
            List<Amenity> output = await Task.Run(() => _amenityRepository.Object.GetAllAmenities());
            Assert.AreEqual(output[0].AmenityName, "Water Bottle");
            Assert.AreEqual(output[1].AmenityName, "Charging Port");
        }

        [Test]
        public async Task TestForGetAllAmenitiesByBusId()
        {
            _amenityRepository.Setup(x => x.GetAllAmenitiesByBusId(It.IsAny<int>())).Returns(new List<Amenity>()
            {
                new Amenity()
                {
                    AmenityName="Water Bottle"
                },
                new Amenity()
                {
                    AmenityName="Charging Port"
                }
            });

            List<Amenity> output = await Task.Run(() => _amenityRepository.Object.GetAllAmenitiesByBusId(1));
            Assert.AreEqual(output[0].AmenityName, "Water Bottle");
            Assert.AreEqual(output[1].AmenityName, "Charging Port");
        }



        [Test]
        public async Task TestForPatchAmenity()
        {
            JsonPatchDocument<Amenity> amenity = new JsonPatchDocument<Amenity>();
            _amenityRepository.Setup(x => x.PatchAmenity(It.IsAny<int>(),amenity)).Returns("Updated Successfully");
            string output = await Task.Run(() => _amenityRepository.Object.PatchAmenity(It.IsAny<int>(), amenity));
            Assert.AreEqual(output, "Updated Successfully");
        }


        [Test]
        public async Task TestForPostAmenity()
        {
            Amenity amenity = new Amenity();
            _amenityRepository.Setup(x => x.PostAmenity(amenity)).Returns("Added Successfully");
            string output = await Task.Run(() => _amenityRepository.Object.PostAmenity(amenity));
            Assert.AreEqual(output, "Added Successfully");
        }


        [Test]
        public async Task TestForUpdateAmenity()
        {
            Amenity amenity = new Amenity();
            _amenityRepository.Setup(x => x.UpdateAmenity(It.IsAny<int>(),amenity)).Returns("Updated Successfully");
            string output = await Task.Run(() => _amenityRepository.Object.UpdateAmenity(It.IsAny<int>(), amenity));
            Assert.AreEqual(output, "Updated Successfully");
        }
    }
}
