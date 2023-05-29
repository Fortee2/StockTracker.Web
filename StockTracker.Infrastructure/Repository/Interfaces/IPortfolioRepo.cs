using System;
using StockTracker.Core.Entities;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
	public interface IPortfolioRepo : ICRUDRepo<Portfolio>
	{
        List<Portfolio> RetrieveAllActive();
    }
}
