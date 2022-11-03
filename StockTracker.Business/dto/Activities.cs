using System;
namespace StockTracker.Business.DTO
{
	public class Activities
	{
		public Activities()
		{
		}

        public int SecurityId { get; set; }
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public int Volume { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
    }
}

