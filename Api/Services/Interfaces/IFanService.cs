using Api.Models;

namespace Api.Services.Interfaces
{
    public interface IFanService
    {
        Task<List<Fan>> GetAllFans();
    }
}
