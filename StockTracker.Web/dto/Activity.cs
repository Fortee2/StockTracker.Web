using System;
namespace StockTracker.Web.dto
{
	public class Activity
	{
		public Activity()
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

