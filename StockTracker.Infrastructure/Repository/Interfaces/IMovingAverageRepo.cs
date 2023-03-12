using System;
using StockTracker.Core.Entities;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
	public interface IMovingAverageRepo:IRespository<MovingAverages>
	{
		List<MovingAverages> GetPresetAverages(int ticker_id, DateTime start, DateTime end);
	}
}

