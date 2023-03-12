﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using StockTracker.Core.Calculations.Response;
using StockTracker.Core.Interfaces.Calculations;
using StockTracker.Business.DTO;
using StockTracker.Core.Entities;
using StockTracker.Business.Enumerations;
using  StockTracker.Infrastructure.Repository.Interfaces;
using StockTracker.Business.Services.Interfaces;
using StockTracker.Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockTracker.Web.Controllers
{
    [Route("api/[controller]")]
    public class AveragesController : Controller
    {
        private readonly IAveragesRepo _repo;
        private readonly IAverageService _averageService;
        private readonly ISecuritiesService _securitiesService;
        private readonly IMACDService _macdService;

        public AveragesController(IAveragesRepo repo, IAverageService averageService, IMACDService mACDService, ISecuritiesService securitiesRepo)
        {
            _repo = repo;
            _averageService = averageService;
            _macdService = mACDService;
            _securitiesService = securitiesRepo;
        }

        
        [HttpGet()]
        public List<MADto> GetMovingAverages(int tickerId, AverageTypes averageTypeEnum )
        {
            //TODO:  Fix hard code number of periods.
            return _averageService.RetrieveDataForAverageCalculations(tickerId, averageTypeEnum,9, null);
        }

        [HttpGet("[action]")]
        public  IActionResult UpdateMovingAverages()
        {
            var tickers = _securitiesService.RetriveveAll();
            _averageService.CalculateAllAverages(tickers);

            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult UpdateMACD()
        {
            var tickers = _securitiesService.RetriveveAll();
            _macdService.CalculateMACD(tickers);

            return Ok();
        }
    }
}

