using System;

namespace StockTracker.Business.DTO
{
    public class Securities
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public string Industry { get; set; }

        public string Sector { get; set; }
    }
}

