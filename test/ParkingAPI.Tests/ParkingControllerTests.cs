using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Parking.API.Controllers;
using Parking.API.Repositories;
using Parking.Core.Models;
using Parking.DataSource;
using Xunit;

namespace ParkingAPI.Tests
{
    public class Tests
    {
        private Mock<ILogger<ParkingController>> _loggerMock;
        private IParkingLotRepository _repoMock;
        private ParkingController _parkingController;

        private void Arrange()
        {
            _loggerMock = new Mock<ILogger<ParkingController>>();
            _repoMock = new ParkingLotRepository(new SimpleDictionary());
            _parkingController = new ParkingController(_loggerMock.Object, _repoMock);
        }

        [Fact]
        public void Get_WhenCalled_IsOkResult()
        {
            Arrange();

            var result = _parkingController.Get();

            Assert.IsType<OkObjectResult>(result.Result as OkObjectResult);
        }

        [Fact]
        public void GetById_WhenCalled_IsOkResult()
        {
            Arrange();

            var result = _parkingController.GetById(1);

            Assert.IsType<OkObjectResult>(result.Result as OkObjectResult);
        }

        [Fact]
        public void Put_WhenCalled_IsOkResult()
        {
            Arrange();

            var result = _parkingController.Put(1, 33);

            Assert.IsType<OkObjectResult>(result.Result as OkObjectResult);
        }

        [Fact]
        public void Increment_WhenCalled_IsOkResult()
        {
            Arrange();

            var result = _parkingController.Increment(1);

            Assert.IsType<OkObjectResult>(result.Result as OkObjectResult);
        }

        [Fact]
        public void Decrement_WhenCalled_IsOkResult()
        {
            Arrange();

            var result = _parkingController.Decrement(1);

            Assert.IsType<OkObjectResult>(result.Result as OkObjectResult);
        }

        [Fact]
        public void GetById_WhenCalled_IsNotFoundResult()
        {
            Arrange();

            var result = _parkingController.GetById(5);

            Assert.IsType<NotFoundObjectResult>(result.Result as NotFoundObjectResult);
        }

        [Fact]
        public void Put_WhenCalled_IsNotFoundResult()
        {
            Arrange();

            var result = _parkingController.Put(5, 33);

            Assert.IsType<NotFoundObjectResult>(result.Result as NotFoundObjectResult);
        }

        [Fact]
        public void Increment_WhenCalled_IsNotFoundResult()
        {
            Arrange();

            var result = _parkingController.Increment(5);

            Assert.IsType<NotFoundObjectResult>(result.Result as NotFoundObjectResult);
        }

        [Fact]
        public void Decrement_WhenCalled_IsNotFoundResult()
        {
            Arrange();

            var result = _parkingController.Decrement(5);

            Assert.IsType<NotFoundObjectResult>(result.Result as NotFoundObjectResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsCorrectList()
        {
            Arrange();

            var result = _parkingController.Get().Result as OkObjectResult;

            var parkingLots = Assert.IsType<List<ParkingLot>>(result.Value as List<ParkingLot>);
            Assert.Equal(3, parkingLots.Count());
        }

        [Fact]
        public void GetById_WhenCalled_ReturnsCorrectObject()
        {
            Arrange();

            var result = _parkingController.GetById(1).Result as OkObjectResult;

            var parkingLot = Assert.IsType<ParkingLot>(result.Value as ParkingLot);
            Assert.Equal(1, parkingLot.Id);
        }

        [Fact]
        public void Put_WhenCalled_UpdatesCorrectly()
        {
            Arrange();

            var result = _parkingController.Put(1, 33).Result as OkObjectResult;

            var count = Assert.IsType<int>(result.Value);
            Assert.Equal(33, count);
        }

        [Fact]
        public void Increment_WhenCalled_UpdatesCorrectly()
        {
            Arrange();

            var result = _parkingController.Increment(1).Result as OkObjectResult;


            var count = Assert.IsType<int>(result.Value);
            Assert.Equal(26, count);
        }

        [Fact]
        public void Decrement_WhenCalled_UpdatesCorrectly()
        {
            Arrange();

            var result = _parkingController.Decrement(1).Result as OkObjectResult;

            var count = Assert.IsType<int>(result.Value);
            Assert.Equal(24, count);
        }
    }
}