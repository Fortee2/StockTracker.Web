using System;
namespace StockTracker.Domain.dto
{
	public class EMADto
	{
        public EMADto( DateTime activityDate, decimal previousEMA, decimal calculateValue) : base()
        {
            ActivityDate = activityDate;
            PreviousEMA = previousEMA;
            CalculateValue = calculateValue;
        }

        //Properties
        public DateTime ActivityDate { get; set; }
        public decimal PreviousEMA { get; set; }
        public decimal CalculateValue { get; set; }
    }
}

