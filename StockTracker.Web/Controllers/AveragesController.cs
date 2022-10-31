using System;
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
using StockTracker.Domain.dto;
using StockTracker.Domain.DTO;
using StockTracker.Domain.Entities;
using StockTracker.Domain.Enumerations;
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
        private ISecuritiesRepo _securitiesRepo;
        private readonly Dictionary<ushort, AverageTypes> _hashTable = new Dictionary<ushort, AverageTypes>();

        public AveragesController(IAveragesRepo repo, IAverageService averageService, ISecuritiesRepo securitiesRepo)
        {
            _repo = repo;
            _averageService = averageService;
            _securitiesRepo = securitiesRepo;

            LoadDictionary();
        }

        
        [HttpGet()]
        public List<MADto> GetMovingAverages(int tickerId, AverageTypes averageTypeEnum )
        {
            return _repo.RetrieveDataForPriceCalculations(tickerId, averageTypeEnum);
        }

        [HttpGet("[action]")]
        public  IActionResult UpdateMovingAverages()
        {
            var tickers = _securitiesRepo.RetriveveAll();
            ProcessAverages(tickers);

            return Ok();
        }

        private void ProcessAverages(List<Securities> tickers)
        {
           
            foreach (var i in _hashTable)
            {
                List<Averages> averageRange = new List<Averages>();

                foreach (var symbol in tickers)
                {
                    var data = _repo.RetrieveDataForPriceCalculations(symbol.Id, i.Value);

                    if (data.Count() <= 1) continue;  //Averages are up to date or no data available

                    if (i.Value.ToString().Contains("EMA"))
                    {
                        averageRange.AddRange(
                            ConvertToAverageEntity(
                                _averageService.CalculateEMA(data, i.Key),
                                symbol,
                                i.Value
                            )
                        );

                        continue;
                    }

                    averageRange.AddRange(
                        ConvertToAverageEntity(
                            _averageService.CalculateMoveingAverage(data, i.Key),
                            symbol,
                            i.Value
                        )
                    );

                }

                if (averageRange.Count() > 0)
                {
                    _repo.AddRange(averageRange);
                }
            }
        }

        private List<Averages> ConvertToAverageEntity(List<MADto> dtoResults, Securities symbol, AverageTypes averageTypes)
        {
            List<Averages> averageRange = new List<Averages>();

            foreach (var result in dtoResults)
            {
                if (result.PreviousMA == 0) //Newly calculated value
                {
                    averageRange.Add(new Averages()
                    {
                        ActivityDate = result.ActivityDate,
                        TickerId = symbol.Id,
                        Value = result.CalculateValue,
                        AverageType = averageTypes.ToString()
                    });
                }
            }

            return averageRange;
        }

        private List<Averages> ConvertToAverageEntity(List<IResponse> dtoResults, Securities symbol, AverageTypes averageTypes)
        {
            List<Averages> averageRange = new List<Averages>();

            foreach (AverageResponse result in dtoResults)
            {
                averageRange.Add(new Averages()
                {
                    ActivityDate = result.ActivityDate,
                    TickerId = symbol.Id,
                    Value = result.Value,
                    AverageType = averageTypes.ToString()
                });
            }

            return averageRange;
        }

        private void LoadDictionary()
        {
            _hashTable.Add(12, AverageTypes.EMA12);
            _hashTable.Add(26, AverageTypes.EMA26);
            _hashTable.Add(9, AverageTypes.MA9);
            _hashTable.Add(7, AverageTypes.MA7);
            _hashTable.Add(14, AverageTypes.MA14);
            _hashTable.Add(21, AverageTypes.MA21);
            _hashTable.Add(50, AverageTypes.MA50);
        }
    }
}

