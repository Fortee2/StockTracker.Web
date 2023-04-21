using System;
using StockTracker.Business.DTO;
using StockTracker.Core.Entities;

namespace StockTracker.Business.Services.Interfaces
{
	public interface ISecuritiesService
	{
        List<Securities> RetriveveAll();
        Securities? FindSecurityBySymbol(string symbol);
    }
}

