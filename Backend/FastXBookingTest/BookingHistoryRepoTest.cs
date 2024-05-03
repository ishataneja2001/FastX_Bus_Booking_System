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
    public class BookingHistoryRepoTest
    {
        private Mock<IBookingHistoryRepository> _bookingHistoryRepository;

        [SetUp]
        public void Setup()
        {
            _bookingHistoryRepository = new Mock<IBookingHistoryRepository>();
        }



        [Test]
        public async Task TestForGetAllBoardingbyBusId()
        {
            _bookingHistoryRepository.Setup(x => x.GetAll()).Returns(new List<BookingHistory>()
            {
                new BookingHistory()
                {
                   UserName = "Ram",
                   Gender = "Male",
                   BusName = "Volvo"
                },
                new BookingHistory()
                {
                   UserName = "Sanju",
                   Gender = "Male",
                   BusName = "Super Luxury"
                }
            });
            List<BookingHistory> output = await Task.Run(() => _bookingHistoryRepository.Object.GetAll());
            Assert.AreEqual(output[0].UserName, "Ram");
            Assert.AreEqual(output[0].Gender, "Male");
            Assert.AreEqual(output[1].UserName, "Sanju");
            Assert.AreEqual(output[1].Gender, "Male");
        }



        
    }
}
