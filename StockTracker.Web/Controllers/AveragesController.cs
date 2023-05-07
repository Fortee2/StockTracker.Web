using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;   
using StockTracker.Business.DTO;
using StockTracker.Business.Enumerations;
using StockTracker.Business.Services.Interfaces;
using StockTracker.Infrastructure.Repository.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockTracker.Web.Controllers
{
    [Route("api/[controller]")]
    public class AveragesController : Controller
    {
        private readonly IAverageService _averageService;
        private readonly ISecuritiesService _securitiesService;
        private readonly IMACDService _macdService;

        public AveragesController(IAveragesRepo repo, IAverageService averageService, IMACDService mACDService, ISecuritiesService securitiesRepo)
        {
            _averageService = averageService;
            _macdService = mACDService;
            _securitiesService = securitiesRepo;
        }

         
        [HttpGet()]
        public List<MADto> GetMovingAverages(int tickerId, AverageTypes averageTypeEnum )
        {
            return _averageService.RetrieveDataForAverageCalculations(tickerId, averageTypeEnum, null);
        }

        [HttpGet("[action]")]
        public IActionResult UpdateMovingAverages(string symbol)
        {
            try
            {
                var tickers = _securitiesService.FindSecurityBySymbol(symbol);
                _averageService.CalculateAllAveragesBySymbol(tickers);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("[action]")]
        public IActionResult UpdateMACD(string symbol)
        {
            var tickers = _securitiesService.FindSecurityBySymbol(symbol);
            _macdService.CalculateMACD(tickers);

            return Ok();
        }
    }
}

