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
    public class BookingRepoTest
    {
        private Mock<IBookingRepository> _bookingRepository;

        [SetUp]
        public void Setup()
        {
            _bookingRepository = new Mock<IBookingRepository>();
        }


        [Test]
        public async Task TestForDeleteBooking()
        {
            _bookingRepository.Setup(x => x.DeleteBooking(It.IsAny<int>())).Returns("Deleted Successfully");
            string output = await Task.Run(() => _bookingRepository.Object.DeleteBooking(2));
            Assert.AreEqual(output, "Deleted Successfully");
        }


        [Test]
        public async Task TestForGetAllBookingbyBusId()
        {
            _bookingRepository.Setup(x => x.GetAllBookingsByBusId(It.IsAny<int>())).Returns(new List<Booking>()
            {
                new Booking()
                {
                   UserId = 1,
                   BusId = 1,
                },
                new Booking()
                {
                   UserId = 1,
                   BusId = 2,
                }
            });
            List<Booking> output = await Task.Run(() => _bookingRepository.Object.GetAllBookingsByBusId(2));
            Assert.AreEqual(output[0].UserId, 1);
            Assert.AreEqual(output[0].BusId, 1);
            Assert.AreEqual(output[1].UserId, 1);
            Assert.AreEqual(output[1].BusId, 2);
        }



        [Test]
        public async Task TestForPostBooking()
        {
            Booking booking = new Booking();
            _bookingRepository.Setup(x => x.PostBooking(booking)).Returns(new Booking()
            {
                UserId = 1,
                BusId = 1,
            });
            Booking book = await Task.Run(() => _bookingRepository.Object.PostBooking(booking));
            Assert.AreEqual(book.UserId, 1);
            Assert.AreEqual(book.BusId, 1);
        }


        [Test]
        public async Task TestForGetBookingByUserId()
        {
            _bookingRepository.Setup(x => x.GetAllBookingsByUserId(It.IsAny<int>())).Returns(new List<Booking>()
            {
                new Booking()
                {
                   UserId = 1,
                   BusId = 1,
                },
                new Booking()
                {
                   UserId = 1,
                   BusId = 2,
                }
            });
            List<Booking> output = await Task.Run(() => _bookingRepository.Object.GetAllBookingsByUserId(2));
            Assert.AreEqual(output[0].UserId, 1);
            Assert.AreEqual(output[0].BusId, 1);
            Assert.AreEqual(output[1].UserId, 1);
            Assert.AreEqual(output[1].BusId, 2);
        }
    }
}
