using System;
using StockTracker.Business.DTO;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Business.Services.Interfaces
{
	public interface IChartService
	{

        List<CandleStickChart> RetrieveCandleSticks(int securityId, int numberOfPeriods);

        List<Activities> RetrieveForId(int securityId, int numberOfPeriods);
    }
}

