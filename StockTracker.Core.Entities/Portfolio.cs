namespace StockTracker.Core.Entities
{
    public class Portfolio
    {
        public int Id { get; set; }
        public int TickerId { get; set; }   
        public bool Active { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual Ticker? Ticker { get; set; }
    }
}