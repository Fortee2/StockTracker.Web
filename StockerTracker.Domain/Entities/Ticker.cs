using System;
using System.Collections.Generic;

#nullable disable

namespace StockTracker.Domain.Entities
{
    public partial class Ticker
    {
        public Ticker()
        {
            Rsis = new HashSet<Rsi>();
        }

        public int Id { get; set; }
        public string Symbol { get; set; }
        public string TickerName { get; set; }
        public string Trend { get; set; }
        public float? Close { get; set; }
        public byte InPortfolio { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }

        public virtual ICollection<Rsi> Rsis { get; set; }
    }
}
