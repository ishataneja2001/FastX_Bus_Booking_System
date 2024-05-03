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
    public class DroppingPointRepoTest
    {
        private Mock<IDroppingPointRepository> _droppingPointRepository;

        [SetUp]
        public void Setup()
        {
            _droppingPointRepository = new Mock<IDroppingPointRepository>();
        }


        [Test]
        public async Task TestForDeleteDroppingPoint()
        {
            _droppingPointRepository.Setup(x => x.DeleteDroppingPoints(It.IsAny<int>())).Returns("Deleted Successfully");
            string output = await Task.Run(() => _droppingPointRepository.Object.DeleteDroppingPoints(2));
            Assert.AreEqual(output, "Deleted Successfully");
        }


        [Test]
        public async Task TestForGetAllDroppingbyBusId()
        {
            _droppingPointRepository.Setup(x => x.GetDroppingPointsByBusId(It.IsAny<int>())).Returns(new List<DroppingPoint>()
            {
                new DroppingPoint()
                {
                   PlaceName = "Hyderabad",
                   BusId = 2,
                },
                new DroppingPoint()
                {
                    PlaceName = "Secunderabad",
                    BusId = 1,
                }
            });
            List<DroppingPoint> output = await Task.Run(() => _droppingPointRepository.Object.GetDroppingPointsByBusId(2));
            Assert.AreEqual(output[0].PlaceName, "Hyderabad");
            Assert.AreEqual(output[0].BusId, 2);
            Assert.AreEqual(output[1].PlaceName, "Secunderabad");
            Assert.AreEqual(output[1].BusId, 1);
        }



        [Test]
        public async Task TestForPatchDroppingPoint()
        {
            JsonPatchDocument<DroppingPoint> jsonpatch = new JsonPatchDocument<DroppingPoint>();
            _droppingPointRepository.Setup(x => x.PatchDroppingPoint(It.IsAny<int>(), jsonpatch)).Returns("Updated Successfully");
            string output = await Task.Run(() => _droppingPointRepository.Object.PatchDroppingPoint(It.IsAny<int>(), jsonpatch));
            Assert.AreEqual(output, "Updated Successfully");
        }


        [Test]
        public async Task TestForPosDroppingPoint()
        {
            DroppingPoint droppingPoint = new DroppingPoint();
            _droppingPointRepository.Setup(x => x.PostDroppingPoint(droppingPoint)).Returns("Added Successfully");
            string output = await Task.Run(() => _droppingPointRepository.Object.PostDroppingPoint(droppingPoint));
            Assert.AreEqual(output, "Added Successfully");
        }


        [Test]
        public async Task TestForUpdateDroppingPoint()
        {
            DroppingPoint droppingPoint = new DroppingPoint();
            _droppingPointRepository.Setup(x => x.UpdateDroppingPoints(It.IsAny<int>(), droppingPoint)).Returns("Updated Successfully");
            string output = await Task.Run(() => _droppingPointRepository.Object.UpdateDroppingPoints(It.IsAny<int>(), droppingPoint));
            Assert.AreEqual(output, "Updated Successfully");
        }
    }
}
