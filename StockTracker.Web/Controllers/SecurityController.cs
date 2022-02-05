using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockTracker.Web.Repository.intefaces;
using StockTracker.Domain.DTO;

namespace StockTracker.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly ISecuritiesRepo securitiesLogic;
        private readonly ILogger<SecurityController> _logger;

        public SecurityController(ILogger<SecurityController> logger,
            ISecuritiesRepo businessLogic)
        {
            _logger = logger;
            securitiesLogic = businessLogic;
        }

        [HttpGet]
        public List<Securities> Get()
        {
            return securitiesLogic.RetriveveAll();
         
        }
    }
}
