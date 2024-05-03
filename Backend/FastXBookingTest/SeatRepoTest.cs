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
    public class SeatRepoTest
    {
        private Mock<ISeatRepository> _seatRepository;

        [SetUp]
        public void Setup()
        {
            _seatRepository = new Mock<ISeatRepository>();
        }


        

        [Test]
        public async Task TestForGetAllSeatsbyBookingId()
        {
            _seatRepository.Setup(x => x.GetSeatsByBookingId(It.IsAny<int>())).Returns(new List<Seat>()
            {
                new Seat()
                {
                   PassengerName = "Shyam",
                   Gender = "Male",
                   Amount = 500
                },
                new Seat()
                {
                   PassengerName = "Kiran",
                   Gender = "Male",
                   Amount = 500
                },
            });
            List<Seat> output = await Task.Run(() => _seatRepository.Object.GetSeatsByBookingId(2));
            Assert.AreEqual(output[0].PassengerName, "Shyam");
            Assert.AreEqual(output[0].Gender, "Male");
            Assert.AreEqual(output[0].Amount, 500);
            Assert.AreEqual(output[1].PassengerName, "Kiran");
            Assert.AreEqual(output[1].Gender, "Male");
            Assert.AreEqual(output[1].Amount, 500);
        }



        [Test]
        public async Task TestForGetAllSeatsbyUserId()
        {
            _seatRepository.Setup(x => x.GetSeatsByUserId(It.IsAny<int>())).Returns(new List<Seat>()
            {
                new Seat()
                {
                   PassengerName = "Shyam",
                   Gender = "Male",
                   Amount = 500
                },
                new Seat()
                {
                   PassengerName = "Kiran",
                   Gender = "Male",
                   Amount = 500
                },
            });
            List<Seat> output = await Task.Run(() => _seatRepository.Object.GetSeatsByUserId(2));
            Assert.AreEqual(output[0].PassengerName, "Shyam");
            Assert.AreEqual(output[0].Gender, "Male");
            Assert.AreEqual(output[0].Amount, 500);
            Assert.AreEqual(output[1].PassengerName, "Kiran");
            Assert.AreEqual(output[1].Gender, "Male");
            Assert.AreEqual(output[1].Amount, 500);
        }


    }
}
