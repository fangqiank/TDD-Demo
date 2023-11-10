using Api.Controllers;
using Api.Models;
using Api.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tests.Fixtures;

namespace Tests.Systems.Controllers
{
    public class TestfansController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200()
        {
            //Arrange
            var mockFanService = new Mock<IFanService>();
            mockFanService.Setup(s => s.GetAllFans())
                .ReturnsAsync(FansFixture.GetFans());
                

            var fansController = new FansController(mockFanService.Object);

            //Act
            var result = (OkObjectResult)await fansController.Get();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokeService()
        {
            //Arrange
            var mockFanService = new Mock<IFanService>();
            mockFanService.Setup(s => s.GetAllFans())
                .ReturnsAsync(FansFixture.GetFans());

            var fansController = new FansController(mockFanService.Object);

            //Act
            var result = (OkObjectResult)await fansController.Get();

            //Assert
            mockFanService.Verify(s => s.GetAllFans(), Times.Once());
        }

        [Fact]
        public async Task Get_OnSuccess_ReturListOfFans()
        {
            //Arrange
            var mockFanService = new Mock<IFanService>();
            mockFanService.Setup(s => s.GetAllFans())
                .ReturnsAsync(FansFixture.GetFans());

            var fansController = new FansController(mockFanService.Object);

            //Act
            var result = (OkObjectResult)await fansController.Get();

            //Assert
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().BeOfType<List<Fan>>();
        }

        [Fact]
        public async Task Get_OnNoFans_ReturnNotFound()
        {
            //Arrange
            var mockFanService = new Mock<IFanService>();
            mockFanService.Setup(s => s.GetAllFans())
                .ReturnsAsync(new List<Fan>());

            var fansController = new FansController(mockFanService.Object);

            //Act
            var result = (NotFoundResult)await fansController.Get();

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
