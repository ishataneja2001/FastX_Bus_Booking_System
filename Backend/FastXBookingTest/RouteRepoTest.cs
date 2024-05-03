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
    public class RouteRepoTest
    {
        private Mock<IRouteRepository> _routeRepository;

        [SetUp]
        public void Setup()
        {
            _routeRepository = new Mock<IRouteRepository>();
        }


        [Test]
        public async Task TestForDeleteRoute()
        {
            _routeRepository.Setup(x => x.DeleteBusRoute(It.IsAny<int>())).Returns("Deleted Successfully");
            string output = await Task.Run(() => _routeRepository.Object.DeleteBusRoute(2));
            Assert.AreEqual(output, "Deleted Successfully");
        }


        [Test]
        public async Task TestForGetAllRoutebyBusId()
        {
            _routeRepository.Setup(x => x.GetRoutesByBusId(It.IsAny<int>())).Returns(new List<Route>()
            {
                new Route()
                {
                   PlaceName = "Hyderabad",
                   BusId = 2,
                },
                new Route()
                {
                    PlaceName = "Secunderabad",
                    BusId = 1,
                }
            });
            List<Route> output = await Task.Run(() => _routeRepository.Object.GetRoutesByBusId(2));
            Assert.AreEqual(output[0].PlaceName, "Hyderabad");
            Assert.AreEqual(output[0].BusId, 2);
            Assert.AreEqual(output[1].PlaceName, "Secunderabad");
            Assert.AreEqual(output[1].BusId, 1);
        }



        [Test]
        public async Task TestForPatchRoute()
        {
            JsonPatchDocument<Route> jsonpatch = new JsonPatchDocument<Route>();
            _routeRepository.Setup(x => x.PatchRoute(It.IsAny<int>(), jsonpatch)).Returns("Updated Successfully");
            string output = await Task.Run(() => _routeRepository.Object.PatchRoute(It.IsAny<int>(), jsonpatch));
            Assert.AreEqual(output, "Updated Successfully");
        }


        [Test]
        public async Task TestForPostRoute()
        {
            Route route = new Route();
            _routeRepository.Setup(x => x.PostBusRoute(route)).Returns("Added Successfully");
            string output = await Task.Run(() => _routeRepository.Object.PostBusRoute(route));
            Assert.AreEqual(output, "Added Successfully");
        }


        [Test]
        public async Task TestForUpdateRoute()
        {
            Route route = new Route();
            _routeRepository.Setup(x => x.UpdateBusRoute(It.IsAny<int>(), route)).Returns("Updated Successfully");
            string output = await Task.Run(() => _routeRepository.Object.UpdateBusRoute(It.IsAny<int>(), route));
            Assert.AreEqual(output, "Updated Successfully");
        }
    }
}
