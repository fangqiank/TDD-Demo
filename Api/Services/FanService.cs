using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services
{
    public class FanService : IFanService
    {
        private readonly HttpClient _httpClient;

        public FanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Fan>> GetAllFans()
        {
            throw new NotImplementedException();
        }
    }
}
