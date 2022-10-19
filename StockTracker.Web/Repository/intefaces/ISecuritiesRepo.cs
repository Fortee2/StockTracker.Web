﻿using System;
using System.Collections.Generic;

namespace StockTracker.Web.Repository.intefaces
{
    public interface ISecuritiesRepo:IRespoitory<Domain.Entities.Ticker, Domain.DTO.Securities>
    {
        List<Domain.DTO.Securities> RetriveveAll();
    }
}