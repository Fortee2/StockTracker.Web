using System;
namespace StockTracker.Domain.DTO
{
	public class CandleStickChart
	{
		public CandleStickChart()
		{

		}

		public DateTime Date { get; set; }
		public decimal Low { get; set; }
		public decimal Open { get; set; }
		public decimal Close { get; set; }
		public decimal High { get; set; }
	}
}

