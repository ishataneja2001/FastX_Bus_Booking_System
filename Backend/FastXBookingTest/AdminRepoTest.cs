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
    public class AdminRepoTest
    {
        private Mock<IAdminRepository> _adminRepository;

        [SetUp]
        public void Setup()
        {
            _adminRepository = new Mock<IAdminRepository>(MockBehavior.Strict);
        }


        [Test]
        public async Task TestForGetAdmin()
        {
            _adminRepository.Setup(r => r.GetAllAdmin()).Returns(new List<User>()
            {
                new User
                {
                    UserId= 1,
                    Name = "Sample1",
                    Email = "sample1@gmail.com",
                    Password="Sample1"
                },
                 new User
                {
                    UserId= 2,
                    Name = "Sample2",
                    Email = "sample2@gmail.com",
                    Password="Sample2"
                }
            });
            IAdminRepository adminRepository = _adminRepository.Object;
            List<User> users = await Task.Run(() => adminRepository.GetAllAdmin());
            Assert.AreEqual(users[0].Name, "Sample1");
            Assert.AreEqual(users[0].Email, "sample1@gmail.com");
            Assert.AreEqual(users[1].Name, "Sample2");
            Assert.AreEqual(users[1].Email, "sample2@gmail.com");
        }


        [Test]
        public async Task TestForDeleteAdmin()
        {
            _adminRepository.Setup(r => r.DeleteAdmin(It.IsAny<int>())).Returns("Deleted Successfully");
            string output = await Task.Run(() => _adminRepository.Object.DeleteAdmin(2));
            Assert.AreEqual(output, "Deleted Successfully");
        }


        [Test]
        public async Task TestForPostAdmin()
        {
            User user = new User();
            _adminRepository.Setup(r => r.PostAdmin(user)).Returns("Added Successfully");
            string output = await Task.Run(() => _adminRepository.Object.PostAdmin(user));
            Assert.AreEqual(output, "Added Successfully");
        }


        [Test]
        public async Task TestForUpdatingAdmin()
        {
            User user = new User();
            _adminRepository.Setup(r => r.ModifyAdminDetails(It.IsAny<int>(), user)).Returns("Updated Successfully");
            string output = await Task.Run(() => _adminRepository.Object.ModifyAdminDetails(It.IsAny<int>(), user));
            Assert.AreEqual(output, "Updated Successfully");
        }

        [Test]
        public async Task TestForPatchAdmin()
        {
            JsonPatchDocument<User> user = new JsonPatchDocument<User>();
            _adminRepository.Setup(r => r.PatchAdmin(It.IsAny<int>(), user)).Returns("Updated Successfully");
            string output = await Task.Run(() => _adminRepository.Object.PatchAdmin(It.IsAny<int>(), user));
            Assert.AreEqual(output, "Updated Successfully");
        }
    }
}
