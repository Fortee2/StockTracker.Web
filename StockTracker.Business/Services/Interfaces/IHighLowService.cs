namespace StockTracker.Business.Services.Interfaces
{
    public interface IHighLowService{
        HighLowResult[] CalculateHighLow(Dictionary<DateTime, decimal> Prices, ushort numSessions);
    }
}