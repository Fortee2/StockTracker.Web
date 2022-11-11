using System;
using StockTracker.Domain.Entities;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
	public interface IMovingAverageRepo:IRespository<MovingAverages>
	{
		List<MovingAverages> GetPresetAverages(int ticker_id, DateTime start, DateTime end);
	}
}

