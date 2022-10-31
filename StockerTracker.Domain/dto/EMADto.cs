using System;
namespace StockTracker.Domain.dto
{
	public class MADto
	{
        public MADto( DateTime activityDate, decimal previousMA, decimal calculateValue) : base()
        {
            ActivityDate = activityDate;
            PreviousMA = previousMA;
            CalculateValue = calculateValue;
        }

        //Properties
        public DateTime ActivityDate { get; set; }
        public decimal PreviousMA { get; set; }
        public decimal CalculateValue { get; set; }
    }
}

