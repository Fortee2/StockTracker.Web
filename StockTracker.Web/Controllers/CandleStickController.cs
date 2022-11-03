using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockTracker.Business.DTO;
using StockTracker.Business.Services.Interfaces;
using  StockTracker.Infrastructure.Repository.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockTracker.Web.Controllers
{
    [Route("api/[controller]")]
    public class CandleStickController : Controller
    {

        private readonly ILogger<CandleStickController> log;
        private readonly IChartService _chartService;

        public CandleStickController(IChartService chartService, ILogger<CandleStickController> logger)
        {
            log = logger;
            _chartService = chartService;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public List<CandleStickChart> GetForCandleStick(int id)
        {
            //TODO: Update the front end to all number of days to passed in
            return _chartService.RetrieveCandleSticks(id, 90);
        }

    }
}

