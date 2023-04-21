using System;
using StockTracker.Core.Calculations;
using StockTracker.Core.Domain;
using StockTracker.Core.Entities;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Business.Services.Interfaces
{
	public interface IRelativeStrengthService
    {
        List<RelativeStrength> CalculateRSI(string symbol);

        Task UpdateRsiForSymbol(string symbol);

        Task SaveRSI(Ticker security);

        DateTime? RetrieveLast(int tickerId);

    }
}

