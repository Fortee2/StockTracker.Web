using System;
using StockTracker.Business.dto;
using StockTracker.Core.Calculations;
using StockTracker.Core.Domain;
using System.Linq;

namespace StockTracker.Business.Services
{
    public class SlopeService
    {
        public SlopeService()
        {
           
        }

        public SlopeDto CalculateSlope(IList<SlopeData> prices)
        {
            Slope slopeCalculator = new Slope(prices);
            return new SlopeDto(prices.Last().ActivityDate, slopeCalculator.Calculate());
        }

        public IList<SlopeDto> CalculateSlopes(IList<SlopeData> prices, int numberOfPeriods = 7)
        {            
            List<SlopeDto> slopes = new List<SlopeDto>();
 
            for (int i = 0; i < prices.Count; i=  i + numberOfPeriods)
            {
                List<SlopeData> data = prices.Skip(i).Take(numberOfPeriods).ToList();   
                Slope slopeCalculator = new Slope(data);
                SlopeDto slope = new SlopeDto(data.Last().ActivityDate, slopeCalculator.Calculate());
                slopes.Add(slope);
            }

            return slopes;
        }
    }
}