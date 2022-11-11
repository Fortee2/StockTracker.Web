using System;
using System.Collections.Generic;
using StockTracker.Database.investing;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
	public interface IRespository<T>
	{
        InvestingContext GetDbContext();

		T FindById(int Id);
	}
}

