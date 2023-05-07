using System;
using StockTracker.Infrastructure.Investing;
using StockTracker.Core.Entities;
using StockTracker.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace StockTracker.Infrastructure.Repository
{
	public class MovingAveragesRepo:IMovingAverageRepo
	{
		private readonly InvestingContext _dbContext;

		public MovingAveragesRepo(InvestingContext dbContext)
		{
			_dbContext = dbContext;
		}

        public List<EmaResult> GetPresetAverages(int ticker_id, DateTime start, DateTime end)
        {            
            string sql = @"
                SELECT ema12.ticker_id as tickerid, ema12.activity_date as activitydate, ema12.`value` as ema12, ema26.`value` as ema26
                FROM investing.averages ema12
                LEFT JOIN investing.averages ema26 ON ema12.ticker_id = ema26.ticker_id AND ema12.activity_date = ema26.activity_date
                WHERE ema12.average_type = 'EMA12'
                AND ema26.average_type = 'EMA26'
                AND ema12.ticker_id = {0}";

            var results = _dbContext.EmaResults.FromSqlRaw(String.Format(sql, ticker_id)).ToList();

            return results;
        }

        public EmaResult FindById(int id)
        {
            throw new NotImplementedException();
        }

        public InvestingContext GetDbContext()
        {
			return _dbContext;
        }
    }
}

