using System;
using System.Collections.Generic;
using StockTracker.Database.investing;
using StockTracker.Domain.Entities;
using System.Linq;
using StockTracker.Domain.Enumerations;
using StockTracker.Domain.dto;
using StockTracker.Web.Repository.Interfaces;
using StockTracker.Core.Domain;

namespace StockTracker.Web.Repository
{
	public partial class AveragesRepository:BaseRepository<Averages>, IAveragesRepo
    {
		public AveragesRepository(InvestingContext investingContext):base(investingContext)
		{
		}

		public MADto RetrieveLastAverage(int tickerId, AverageTypes averageType)
		{
			var data = (from ema in _dbContext.Averages
					   where ema.AverageType == averageType.ToString()
							&& ema.TickerId == tickerId
                       orderby ema.ActivityDate descending
					   select (new MADto(ema.ActivityDate, ema.Value ?? 0, 0)))
					   .Take(1)
					   ;


			return data.SingleOrDefault();
		}

		public List<MADto> RetrieveDataForPriceCalculations(int tickerId, AverageTypes averageType)
		{
			var collection = new List<MADto>();

			MADto previous = RetrieveLastAverage(tickerId, averageType);
			DateTime startTime =  (previous == null) ? DateTime.UnixEpoch : previous.ActivityDate;

			if (previous != null) collection.Add(previous);

            var data = (from quotes in _dbContext.Activities
                        where (
							quotes.TickerId == tickerId
								&& quotes.ActivityDate > startTime
							)
                        orderby quotes.ActivityDate
                        select (new MADto(quotes.ActivityDate, 0, quotes.Close)))
			;

			collection.AddRange(data.ToList());

			return collection;
        }
    }
}

