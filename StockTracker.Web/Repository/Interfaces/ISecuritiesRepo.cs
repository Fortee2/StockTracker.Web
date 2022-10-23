using System;
using System.Collections.Generic;

namespace StockTracker.Web.Repository.Interfaces
{
    public interface ISecuritiesRepo:IRespoitory<Domain.Entities.Ticker>
    {
        List<Domain.DTO.Securities> RetriveveAll();
    }
}
