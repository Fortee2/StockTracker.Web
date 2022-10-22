using System;
using StockTracker.Database.investing;
using StockTracker.Domain.Entities;

namespace StockTracker.Web.Repository
{
	public abstract class AveragesRepository:BaseRepository<Averages>
	{
		public AveragesRepository(InvestingContext investingContext):base(investingContext)
		{
		}
	}
}

