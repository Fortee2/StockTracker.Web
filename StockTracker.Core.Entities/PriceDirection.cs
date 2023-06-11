namespace StockTracker.Core.Entities
{
    public class PriceDirection
    {
        public PriceDirection()
        {
            ActivityDate = System.DateTime.Now;
            Direction = string.Empty;
        }

        public int Id { get; set; }
        public int TickerId { get; set; }   
        public string Direction { get; set; }
        public DateTime ActivityDate { get; set; }
        public virtual Ticker? Ticker { get; set; }
    }
}