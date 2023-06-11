using System;
using StockTracker.Business.dto;
using StockTracker.Core.Calculations;
using StockTracker.Core.Domain;
using System.Linq;
using StockTracker.Business.Services.Interfaces;
using StockTracker.Infrastructure.Repository.Interfaces;
using StockTracker.Business.Enumerations;
using StockTracker.Core.Entities;

namespace StockTracker.Business.Services
{
    public class SlopeService:ISlopeService
    {
        private readonly IPriceDirectionRepo _repo;
        private readonly ISecuritiesService _securitiesService;

        public SlopeService(IPriceDirectionRepo priceDirectionRepo, ISecuritiesService securitiesService)
        {
           _repo = priceDirectionRepo;
           _securitiesService = securitiesService;
        }

        /// <summary>
        /// Calculates the slope of the last 7 days of closing prices based on an Average
        /// </summary>
        public SlopeDto CalculateSlope(AverageTypes averageType, int tickerId, DateTime tradeDate, int numberOfPeriods = 7)
        {
            var prices = RetrieveAverageForCalculations(tickerId, averageType, tradeDate, numberOfPeriods);
            Slope slopeCalculator = new Slope(prices);
            return new SlopeDto(prices.Last().ActivityDate, slopeCalculator.Calculate());
        }

        public void SaveSlope(SlopeDto slopeDto, int tickerId)
        {
            var priceDirection = new PriceDirection
            {
                TickerId = tickerId,
                ActivityDate = slopeDto.ActivityDate,
                Direction = slopeDto.Direction.ToString()
            };

            _repo.Add(priceDirection);
        }
        private List<SlopeData> RetrieveAverageForCalculations(int tickerId, AverageTypes averageType, DateTime tradeDate, int numberOfPeriods = 7)
        {
            var collection = new List<SlopeData>();
    
            var data = (from quotes in _repo.GetDbContext().Averages
                        where (
                            quotes.TickerId == tickerId
                                && quotes.AverageType == averageType.ToString()
                                && quotes.ActivityDate <= tradeDate
                            )
                        orderby quotes.ActivityDate
                        select (new SlopeData(quotes.ActivityDate, quotes.Value?? 0.0m))).Take (numberOfPeriods);
            ;

            collection.AddRange(data.ToList());

            return collection;
        }
    }
}