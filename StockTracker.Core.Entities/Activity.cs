using System;
using System.Collections.Generic;

#nullable disable

namespace StockTracker.Core.Entities
{
    public partial class Activity
    {
        public int Id { get; set; }
        public int TickerId { get; set; }
        public DateTime ActivityDate { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public int Volume { get; set; }
        public string Updown { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public string CandlePattern { get; set; }
    }
}
