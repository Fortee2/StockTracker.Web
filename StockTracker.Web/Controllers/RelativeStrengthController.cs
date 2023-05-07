using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Services.Interfaces;
using System.Threading.Tasks;

namespace StockTracker.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelativeStrengthController : ControllerBase
    {
        private readonly IRelativeStrengthService _relativeStrengthService;

        public RelativeStrengthController(IRelativeStrengthService relativeStrengthService)
        {
            _relativeStrengthService = relativeStrengthService;
        }

        [HttpPost("{symbol}")]
        public async Task<IActionResult> CalculateRelativeStrength(string symbol)
        {
            await _relativeStrengthService.UpdateRsiForSymbol(symbol);

            return Ok();
        }
    }
}
