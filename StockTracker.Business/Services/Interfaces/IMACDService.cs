using System;
using StockTracker.Business.DTO;

namespace StockTracker.Business.Services.Interfaces
{
	public interface IMACDService
	{
        void CalculateMACD(List<Securities> tickers);
    }
}

