using Api.Configuration;
using Api.Models;
using Api.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Tests.Fixtures;
using Tests.Helpers;

namespace Tests.Systems.Services
{
    public class TestFanSrvice
    {
        [Fact]
        public async Task GetAllFans_OnInvoked_HttpGet()
        {
            //Arrange
            var url = "https://mywebsite.com/api/v1/fans";
            var response = FansFixture.GetFans();
            var mockHandler = MockHttpHandler<Fan>.SetupGetRequest(response);
            var httpClient = new HttpClient(mockHandler.Object);
            
            var config = Options.Create(new ApiServiceConfig
            {
                Url = url,
            });

            var fanService = new FanService(httpClient, config);

            //Act
            await fanService.GetAllFans();

            //Assert
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(my => 
                    my.Method == HttpMethod.Get && my.RequestUri.ToString() == url),
                ItExpr.IsAny<CancellationToken>()
                );
        }

        [Fact]
        public async Task GetAllFans_OnInvoked_ListOfFans()
        {
            //Arrange
            var url = "https://mywebsite.com/api/v1/fans";
            var response = FansFixture.GetFans();
            var mockHandler = MockHttpHandler<Fan>.SetupGetRequest(response);
            var httpClient = new HttpClient(mockHandler.Object);

            var config = Options.Create(new ApiServiceConfig
            {
                Url = url,
            });

            var fanService = new FanService(httpClient, config);

            //Act
            var result = await fanService.GetAllFans();

            //Assert
           result.Should().BeOfType<List<Fan>>();
        }

        [Fact]
        public async Task GetAllFans_OnInvoked_ReturnEmptyList()
        {
            //Arrange
            var url = "https://mywebsite.com/api/v1/fans";
            var mockHandler = MockHttpHandler<Fan>.SetupReturnNotFound();
            var httpClient = new HttpClient(mockHandler.Object);

            var config = Options.Create(new ApiServiceConfig
            {
                Url = url,
            });

            var fanService = new FanService(httpClient, config);

            //Act
            var result = await fanService.GetAllFans();

            //Assert
           result.Count.Should().Be(0);
        }
    }
}
