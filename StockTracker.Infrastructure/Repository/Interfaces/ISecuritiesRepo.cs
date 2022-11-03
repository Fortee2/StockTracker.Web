using System;
using System.Collections.Generic;
using StockTracker.Domain.Entities;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
    public interface ISecuritiesRepo:IRespoitory<Ticker>
    {
        List<Ticker> RetriveveAll();
    }
}
