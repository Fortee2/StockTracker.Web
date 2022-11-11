using System;
namespace StockTracker.Domain.Entities
{
	public class MovingAverages
	{
		public MovingAverages()
		{
        }

        public int TickerId { get; set; }
        public string TickerName { get; set; }
        public string Symbol { get; set; }
        public DateTime ActivityDate { get; set; }
        public decimal EMA12 { get; set; }
        public decimal? EMA26 { get; set; }
        public decimal? MA9 { get; set; }
        public decimal? Vol90 { get; set; }
        public decimal? MACD { get; set; }
        public decimal? Signal { get; set; }
    }
}

