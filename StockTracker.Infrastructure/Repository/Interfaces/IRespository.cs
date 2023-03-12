using System;
using System.Collections.Generic;
using StockTracker.Infrastructure.Investing;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
	public interface IRespository<T>
	{
        InvestingContext GetDbContext();

		T FindById(int Id);
	}
}

