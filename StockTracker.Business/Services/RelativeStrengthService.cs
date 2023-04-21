using StockTracker.Business.Services.Interfaces;
using StockTracker.Core.Calculations;
using StockTracker.Core.Domain;
using StockTracker.Core.Entities;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Business.Services
{
    public class RelativeStrengthService: IRelativeStrengthService
    {
		private IRsiRepo _rsiRepo;
		private IActivitiesRepo _activitiesRepo;
        private ISecuritiesRepo _securitiesRepo;

        public RelativeStrengthService(IRsiRepo repo, IActivitiesRepo activitiesRepo, ISecuritiesRepo securitiesRepo)
		{
			_rsiRepo = repo;
            _activitiesRepo = activitiesRepo;
            _securitiesRepo = securitiesRepo; 
		}

		public List<RelativeStrength> CalculateRSI (string symbol)
		{
            var security = _securitiesRepo.FindBySymbol(symbol);

            if (security == null) return new List<RelativeStrength>();

            var updatedDate = RetrieveLast(security.Id);
            var data = RetrieveDataForCalculations(security.Id, updatedDate);

            var realitiveStrength = new RealitiveStrengthIndex(data);
            realitiveStrength.Calculate();

            return data;
		}

        public async Task UpdateRsiForSymbol(string symbol)
        {
            var security = _securitiesRepo.FindBySymbol(symbol);

            if (security == null) return;

            await SaveRSI(security);

        }

        public async Task SaveRSI(Ticker security)
        {
            List<RelativeStrength> relativeStrengthIndices = CalculateRSI(security.Symbol);
            List<Rsi> rSIs = new List<Rsi>();

            foreach(var r in relativeStrengthIndices)
            {
                rSIs.Add(new Rsi() { ActivityDate = r.ActivityDate, AvgGain = r.AvgGain, AvgLoss = r.AvgLoss, Rs = 0, Rsi1 = r.RSIndex, TickerId =  security.Id  });
            }

            await _rsiRepo.AddRangeAsync(rSIs);
        }

        public DateTime? RetrieveLast(int tickerId)
        {
            using var dbContext = _activitiesRepo.GetDbContext();

            var data = (from ema in dbContext.Rsis
                        where ema.TickerId == tickerId
                        orderby ema.ActivityDate descending
                        select ema.ActivityDate
                        ).Take(1);

            return data.SingleOrDefault();
        }

        private List<RelativeStrength> RetrieveDataForCalculations(int securityId, DateTime? lastUpdate)
        {
            var collection = new List<RelativeStrength>();

            DateTime startTime = (lastUpdate.HasValue) ? lastUpdate.Value : DateTime.UnixEpoch;

            startTime = CalculateNewStartDate(securityId, startTime, 14);

            using var dbContext = _activitiesRepo.GetDbContext();

            var data = (from quotes in dbContext.Activities
                        where (
                            quotes.TickerId == securityId
                                && quotes.ActivityDate > startTime
                            )
                        orderby quotes.ActivityDate
                        select (new RelativeStrength(quotes.ActivityDate, quotes.Close)));
            ;

            collection.AddRange(data.ToList());

            return collection;
        }

        private DateTime CalculateNewStartDate(int securityId, DateTime lastUpdated, int interval)
        {
            using var dbContext = _activitiesRepo.GetDbContext();

            var dateData = (from dates in dbContext.Activities
                            where dates.TickerId == securityId && dates.ActivityDate < lastUpdated
                            orderby dates.ActivityDate descending
                            select dates.ActivityDate).Skip(interval * 2).Take(1).FirstOrDefault();

            if (dateData == default(DateTime)) return DateTime.UnixEpoch;

            return dateData;
        }

    }
}

