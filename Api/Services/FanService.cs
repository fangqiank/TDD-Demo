using Api.Configuration;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Api.Services
{
    public class FanService : IFanService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiServiceConfig _config;

        public FanService(
            HttpClient httpClient, 
            IOptions<ApiServiceConfig> config
            )
        {
            _httpClient = httpClient;
            _config = config.Value;
        }
        public async Task<List<Fan>> GetAllFans()
        {
            var res = await _httpClient.GetAsync(_config.Url);

            switch (res.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    return new List<Fan>();
                case System.Net.HttpStatusCode.Unauthorized:
                    return null;
                default:
                    {
                        var fans = await res.Content.ReadFromJsonAsync<List<Fan>>();
                        return fans;
                    }
            }
        }
    }
}
