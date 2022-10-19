using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockTracker.Web.Repository.intefaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockTracker.Web.Controllers
{
    [Route("api/[controller]")]
    public class CandleStickController : Controller
    {

        private readonly ILogger<CandleStickController> log;
        private readonly IActivitiesRepo repo;

        public CandleStickController(IActivitiesRepo activitiesRepo, ILogger<CandleStickController> logger)
        {
            repo = activitiesRepo;
            log = logger;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public List<Domain.DTO.CandleStickChart> GetForCandleStick(int id)
        {
            return repo.RetrieveCandleSticks(id);
        }

    }
}

