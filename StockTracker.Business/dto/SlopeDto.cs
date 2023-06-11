using System;
using StockTracker.Business.Enumerations;

namespace StockTracker.Business.dto
{
	public class SlopeDto
	{
		DateTime _activityDate;
		Decimal _averageValue;
        SlopeDirection _direction;

		public SlopeDto(DateTime activityDate, Decimal? averageValue)
		{
			_activityDate = activityDate;
			_averageValue = averageValue ?? 0;
            _direction = averageValue > 0 ? SlopeDirection.Up : SlopeDirection.Down;  
		}

		public DateTime ActivityDate { get { return _activityDate; } }
		public Decimal Value { get { return _averageValue; } }
        public SlopeDirection Direction { get { return _direction; } }
	}
}