using System;
namespace StockTracker.Core.Entities
{
    public partial class Averages
    {
        public Averages()
        {
            ActivityDate = System.DateTime.Now;
            AverageType = string.Empty;
        }
        
        public int? Id { get; set; }
        public int TickerId { get; set; }
        public DateTime ActivityDate { get; set; }
        public decimal? Value { get; set; }
        public String AverageType { get; set; }
    }
}
