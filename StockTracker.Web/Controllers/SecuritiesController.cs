using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockTracker.Web.BL.intefaces;

namespace StockTracker.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecuritiesController : ControllerBase
    {
        private readonly IBusinessLogic<Securities> securitiesLogic;
        private readonly ILogger<SecuritiesController> _logger;

        public SecuritiesController(ILogger<SecuritiesController> logger,
            IBusinessLogic<Securities> businessLogic)
        {
            _logger = logger;
            securitiesLogic = businessLogic;
        }

        [HttpGet]
        public List<Securities> Get()
        {
           return securitiesLogic.RetrieveAll();
         
        }
    }
}
