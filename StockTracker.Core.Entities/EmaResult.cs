using System;
namespace StockTracker.Core.Entities
{
    public class EmaResult
    {
        public int TickerId { get; set; }
        public DateTime ActivityDate { get; set; }
        public decimal Ema12 { get; set; }
        public decimal Ema26 { get; set; }
    }

}

