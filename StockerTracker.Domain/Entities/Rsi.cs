using System;
using System.Collections.Generic;

#nullable disable

namespace StockTracker.Domain.Entities
{
    public partial class Rsi
    {
        public int Id { get; set; }
        public int TickerId { get; set; }
        public decimal AvgLoss { get; set; }
        public decimal AvgGain { get; set; }
        public decimal Rs { get; set; }
        public decimal Rsi1 { get; set; }

        public virtual Ticker Ticker { get; set; }
    }
}
