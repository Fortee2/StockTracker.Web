using System;
using StockTracker.Business.Enumerations;

namespace StockTracker.Business.dto
{
	public class AverageDto
	{
		DateTime _activityDate;
		Decimal _averageValue;
		AverageTypes _averageType;

		public AverageDto(DateTime activityDate, Decimal? averageValue, AverageTypes averageType)
		{
			_activityDate = activityDate;
			_averageValue = averageValue ?? 0;
			_averageType = averageType;
		}

		public DateTime ActivityDate { get { return _activityDate; } }
		public Decimal Value { get { return _averageValue; } }
		public AverageTypes ValueType { get { return _averageType; } }
	}
}

