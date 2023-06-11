namespace StockTracker.Core.Entities
{
    public partial class IndustrySector
    {
        public IndustrySector()
        {
            Industry = string.Empty;
            Sector = string.Empty;
        }
        
        public int Id { get; set; }
        public string Industry { get; set; }
        public string Sector { get; set; }

    }
}