using System;
using System.Collections.Generic;
using StockTracker.Core.Entities;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
    public interface ISecuritiesRepo:IRespository<Ticker>
    {
        List<Ticker> RetriveveAll();
        Ticker? FindBySymbol(string symbol);
    }
}
