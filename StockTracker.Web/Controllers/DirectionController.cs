using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockTracker.Business.Enumerations;
using StockTracker.Business.Services.Interfaces;

namespace StockTracker.Web.Controllers
{
    [Route("api/[controller]")]
    public class DirectionController : Controller
    {

        private readonly ILogger<DirectionController> log;
        private readonly ISlopeService _priceDirectionService;

        public DirectionController(ISlopeService priceDirectionService, ILogger<DirectionController> logger)
        {
            log = logger;
            _priceDirectionService = priceDirectionService;
        }

        // POST api/values
        [HttpPost]
        public void Post(DateTime tradeDate, int tickerId)
        {
            var slope = _priceDirectionService.CalculateSlope(AverageTypes.EMA14, tickerId, tradeDate, 7);
            _priceDirectionService.SaveSlope(slope, tickerId);
        }
    }
}