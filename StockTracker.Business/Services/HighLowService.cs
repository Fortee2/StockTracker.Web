using StockTracker.Business.Services.Interfaces;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Business.Services{

    public class HighLowService : IHighLowService{
        private readonly IActivitiesRepo activitiesRepo;

        public HighLowService(IActivitiesRepo activitiesRepo){
            this.activitiesRepo = activitiesRepo;
        
        }
         
        public  HighLowResult[] CalculateHighLow(Dictionary<DateTime, decimal> Prices, ushort numSessions)
        {
            var highs = new List<HighLowResult>();
            var lows = new List<HighLowResult>();

            foreach (var kvp in Prices.Skip(numSessions - 1))
            {
                var endDate = kvp.Key;
                var startDate = endDate.AddDays(-numSessions + 1);

                var period = Prices.Where(x => x.Key >= startDate && x.Key <= endDate)
                                .Select(x => new object[] { x.Key, x.Value })
                                .ToList();

                var endingPrices = period.Select(x => (Decimal) x[1]).ToList<Decimal>();
                var maxPrice = endingPrices.Max();
                var minPrice = endingPrices.Min();

                highs.Add(new HighLowResult { ActivityDate = endDate, High = maxPrice });
                lows.Add(new HighLowResult { ActivityDate = endDate, Low = minPrice });
            }

            return highs.Zip(lows, (h, l) => new HighLowResult { ActivityDate = h.ActivityDate, High = h.High, Low = l.Low }).ToArray();
        }

    }
}