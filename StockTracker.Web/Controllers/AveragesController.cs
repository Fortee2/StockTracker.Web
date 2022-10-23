using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Domain.dto;
using StockTracker.Web.Repository.Interfaces;
using StockTracker.Web.Services.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockTracker.Web.Controllers
{
    [Route("api/[controller]")]
    public class AveragesController : Controller
    {
        private IAveragesRepo _repo;
        private readonly IAverageService _averageService;

        public AveragesController(IAveragesRepo repo, IAverageService averageService)
        {
            _repo = repo;
            _averageService = averageService;
        }

        
        [HttpGet()]
        public List<EMADto> GetForCalculation()
        {
            var data = _repo.RetrieveDataForPriceCalculations(1, Domain.Enumerations.AverageTypes.EMA12);
            return _averageService.CalculateEMA(data);
        }
    }
}

