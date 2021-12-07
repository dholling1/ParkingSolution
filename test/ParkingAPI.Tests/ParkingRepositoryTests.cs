using System;
using Microsoft.Extensions.Logging;
using Moq;
using Parking.API.Controllers;
using Parking.API.Repositories;
using Parking.DataSource;
using Xunit;

namespace ParkingAPI.Tests
{
    // Yes, I know - this is really testing the data source until I refactor and pull up the logic int the Repository where it should be

    public class ParkingRepositoryTests
    {
        private Mock<ILogger<ParkingController>> _loggerMock;
        private IParkingLotRepository _repoMock;

        private void Arrange()
        {
            _loggerMock = new Mock<ILogger<ParkingController>>();
            _repoMock = new ParkingLotRepository(new SimpleDictionary());
        }

        [Fact]
        public async void Set_WhenCalled_IsFailsTooLow()
        {
            Arrange();

            //Act
            var exception = await Record.ExceptionAsync(() =>
                _repoMock.SetParkingLotCountAsync(1, -1));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async void Set_WhenCalled_IsFailsTooHigh()
        {
            Arrange();

            //Act
            var exception = await Record.ExceptionAsync(() =>
                _repoMock.SetParkingLotCountAsync(1, 101));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async void Set_WhenCalled_IsSucceedMaximum()
        {
            Arrange();

            //Act
            var result = await _repoMock.SetParkingLotCountAsync(1, 100);

            //Assert
            var count = Assert.IsType<int>(result);
            Assert.Equal(100, count);
        }

        [Fact]
        public async void Set_WhenCalled_IsSucceedMinimum()
        {
            Arrange();

            //Act
            var result = await _repoMock.SetParkingLotCountAsync(1, 0);

            //Assert
            var count = Assert.IsType<int>(result);
            Assert.Equal(0, count);
        }

        [Fact]
        public async void Decrementet_WhenCalled_IsFailsTooLow()
        {
            Arrange();

            //Act
            var exception = await Record.ExceptionAsync(() =>
                _repoMock.DecrementParkingLotCountAsync(3));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
        }

        [Fact]
        public async void Incrementet_WhenCalled_IsFailsTooHigh()
        {
            Arrange();

            //Act
            var exception = await Record.ExceptionAsync(() =>
                _repoMock.IncrementParkingLotCountAsync(2));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<InvalidOperationException>(exception);
        }
    }
}
