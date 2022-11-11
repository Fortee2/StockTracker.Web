using System;
using StockTracker.Database.investing;
using StockTracker.Domain.Entities;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Infrastructure.Repository
{
	public class MovingAveragesRepo:IMovingAverageRepo
	{
		private readonly InvestingContext _dbContext;

		public MovingAveragesRepo(InvestingContext dbContext)
		{
			_dbContext = dbContext;
		}

        public List<MovingAverages> GetPresetAverages(int ticker_id, DateTime start, DateTime end)
        {
			var data = (
					from ma in _dbContext.MovingAverages
					where ma.TickerId == ticker_id
						&& ma.ActivityDate >= start
						&& ma.ActivityDate <= end
					select ma
					);

			return data.ToList();
        }

        public MovingAverages FindById(int id)
        {
            return _dbContext.Find<MovingAverages>(id);
        }

        public InvestingContext GetDbContext()
        {
			return _dbContext;
        }
    }
}

