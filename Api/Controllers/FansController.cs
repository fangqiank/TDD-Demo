using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FansController : ControllerBase
    {
        private readonly IFanService _fanService;

        public FansController(IFanService fanService)
        {
            _fanService = fanService;
        }

        [HttpGet(Name = "GetFans")]
        public async Task<IActionResult> Get()
        {
            var fans = await _fanService.GetAllFans();

            if(fans.Any())
                return Ok(fans);

            return NotFound();
        }
    }
}
