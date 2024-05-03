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
    public class BoardingPointRepoTest
    {
        private Mock<IBoardingPointRepository> _boardingPointRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _boardingPointRepositoryMock = new Mock<IBoardingPointRepository>();
        }


        [Test]
        public async Task TestForDeleteBoardingPoint()
        {
            _boardingPointRepositoryMock.Setup(x => x.DeleteBoardingPoints(It.IsAny<int>())).Returns("Deleted Successfully");
            string output = await Task.Run(() => _boardingPointRepositoryMock.Object.DeleteBoardingPoints(2));
            Assert.AreEqual(output, "Deleted Successfully");
        }


        [Test]
        public async Task TestForGetAllBoardingbyBusId()
        {
            _boardingPointRepositoryMock.Setup(x => x.GetBoardingPointsByBusId(It.IsAny<int>())).Returns(new List<BoardingPoint>()
            {
                new BoardingPoint()
                {
                   PlaceName = "Hyderabad",
                   BusId = 2,
                },
                new BoardingPoint()
                {
                    PlaceName = "Secunderabad",
                    BusId = 1,
                }
            }) ;
            List<BoardingPoint> output = await Task.Run(() => _boardingPointRepositoryMock.Object.GetBoardingPointsByBusId(2));
            Assert.AreEqual(output[0].PlaceName, "Hyderabad");
            Assert.AreEqual(output[0].BusId, 2); 
            Assert.AreEqual(output[1].PlaceName, "Secunderabad");
            Assert.AreEqual(output[1].BusId, 1);
        }



        [Test]
        public async Task TestForPatchBoardingPoint()
        {
            JsonPatchDocument<BoardingPoint> jsonpatch = new JsonPatchDocument<BoardingPoint>();
            _boardingPointRepositoryMock.Setup(x => x.PatchBoardingPoint(It.IsAny<int>(), jsonpatch)).Returns("Updated Successfully");
            string output = await Task.Run(() => _boardingPointRepositoryMock.Object.PatchBoardingPoint(It.IsAny<int>(), jsonpatch));
            Assert.AreEqual(output, "Updated Successfully");
        }


        [Test]
        public async Task TestForPostBoardingPoint()
        {
            BoardingPoint boardingPoint = new BoardingPoint();
            _boardingPointRepositoryMock.Setup(x => x.PostBoardingPoint(boardingPoint)).Returns("Added Successfully");
            string output = await Task.Run(() => _boardingPointRepositoryMock.Object.PostBoardingPoint(boardingPoint));
            Assert.AreEqual(output, "Added Successfully");
        }


        [Test]
        public async Task TestForUpdateBoardingPoint()
        {
            BoardingPoint boardingPoint = new BoardingPoint();
            _boardingPointRepositoryMock.Setup(x => x.UpdateBoardingPoints(It.IsAny<int>(), boardingPoint)).Returns("Updated Successfully");
            string output = await Task.Run(() => _boardingPointRepositoryMock.Object.UpdateBoardingPoints(It.IsAny<int>(), boardingPoint));
            Assert.AreEqual(output, "Updated Successfully");
        }

    }
}
