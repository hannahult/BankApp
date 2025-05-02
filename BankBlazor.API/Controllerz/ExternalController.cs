using BankBlazor.API.Servicez;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.API.Controllerz
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        private readonly ExternalApiService _externalApi;

        public ExternalController(ExternalApiService externalApi)
        {
            _externalApi = externalApi;
        }

        [HttpGet("bank-holidays")]
        public async Task<IActionResult> GetNextScottishHoliday()
        {
            var holiday = await _externalApi.GetNextScottishHolidayAsync();
            if (holiday == null) return NotFound();
            return Ok(holiday);
        }
    }
}
