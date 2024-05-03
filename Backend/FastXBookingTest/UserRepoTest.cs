using FastXBookingSample.Interface;
using FastXBookingSample.Models;
using Moq;

namespace FastXBookingTest
{
    [TestFixture]
    public class UserRepoTest
    {
        private Mock<IUserRepository> _mockuserRepository;

        [SetUp]
        public void Setup()
        {
            _mockuserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
        }

        [Test]
        public async Task TestForGetUsers()
        {
            _mockuserRepository.Setup(r => r.GetAllUsers()).Returns(new List<User>()
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
            IUserRepository userRepository = _mockuserRepository.Object;
            List<User> users = await Task.Run(() => userRepository.GetAllUsers());
            Assert.AreEqual(users[0].Name,"Sample1");
            Assert.AreEqual(users[0].Email,"sample1@gmail.com");
            Assert.AreEqual(users[1].Name,"Sample2");
            Assert.AreEqual(users[1].Email,"sample2@gmail.com");
        }


        [Test]
        public async Task TestForDeleteUser()
        {
            _mockuserRepository.Setup(r => r.DeleteUser(It.IsAny<int>())).Returns("Deleted Successfully");
            string output = await Task.Run(() => _mockuserRepository.Object.DeleteUser(2));
            Assert.AreEqual(output, "Deleted Successfully");
        }


        [Test]
        public async Task TestForPostUser()
        {
            User user = new User();
            _mockuserRepository.Setup(r => r.PostUser(user)).Returns("Added Successfully");
            string output = await Task.Run(() => _mockuserRepository.Object.PostUser(user));
            Assert.AreEqual(output, "Added Successfully");
        }


        [Test]
        public async Task TestForUpdatingUser()
        {
            User user = new User();
            _mockuserRepository.Setup(r => r.ModifyUserDetails(It.IsAny<int>(),user)).Returns("Updated Succesfully");
            string output = await Task.Run(() => _mockuserRepository.Object.ModifyUserDetails(It.IsAny<int>(), user));
            Assert.AreEqual(output, "Updated Succesfully");
        }

    }
}