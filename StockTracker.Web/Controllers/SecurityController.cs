using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using  StockTracker.Infrastructure.Repository.Interfaces;
using StockTracker.Business.DTO;
using StockTracker.Business.Services.Interfaces;

namespace StockTracker.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly ISecuritiesService securitiesLogic;
        private readonly ILogger<SecurityController> _logger;

        public SecurityController(ILogger<SecurityController> logger,
            ISecuritiesService businessLogic)
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
