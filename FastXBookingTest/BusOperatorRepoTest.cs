using FastXBookingSample.Interface;
using FastXBookingSample.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastXBookingTest
{
    [TestFixture]
    public class BusOperatorRepoTest
    {
        private Mock<IBusOperatorRepository> _busOperatorRepository;

        [SetUp]
        public void Setup()
        {
            _busOperatorRepository = new Mock<IBusOperatorRepository>(MockBehavior.Strict);
        }


        [Test]
        public async Task TestForGetBusOperator()
        {
            _busOperatorRepository.Setup(r => r.GetAllBusOperators()).Returns(new List<User>()
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
            
            List<User> users = await Task.Run(() => _busOperatorRepository.Object.GetAllBusOperators());
            Assert.AreEqual(users[0].Name, "Sample1");
            Assert.AreEqual(users[0].Email, "sample1@gmail.com");
            Assert.AreEqual(users[1].Name, "Sample2");
            Assert.AreEqual(users[1].Email, "sample2@gmail.com");
        }


        [Test]
        public async Task TestForDeleteBusOperator()
        {
            _busOperatorRepository.Setup(r => r.DeleteBusOperator(It.IsAny<int>())).Returns("Deleted Successfully");
            string output = await Task.Run(() => _busOperatorRepository.Object.DeleteBusOperator(2));
            Assert.AreEqual(output, "Deleted Successfully");
        }


        [Test]
        public async Task TestForPostPostOperator()
        {
            User user = new User();
            _busOperatorRepository.Setup(r => r.PostBusOperator(user)).Returns("Added Successfully");
            string output = await Task.Run(() => _busOperatorRepository.Object.PostBusOperator(user));
            Assert.AreEqual(output, "Added Successfully");
        }


        [Test]
        public async Task TestForUpdatingBusOperator()
        {
            User user = new User();
            _busOperatorRepository.Setup(r => r.ModifyBusOperatorDetails(It.IsAny<int>(), user)).Returns("Updated Successfully");
            string output = await Task.Run(() => _busOperatorRepository.Object.ModifyBusOperatorDetails(It.IsAny<int>(), user));
            Assert.AreEqual(output, "Updated Successfully");
        }
    }
}
