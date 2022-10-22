﻿using System;
using System.Collections.Generic;
using StockTracker.Domain.DTO;
using StockTracker.Domain.Entities;

namespace StockTracker.Web.Repository.intefaces
{
    public interface IActivitiesRepo:IRespoitory<Domain.Entities.Activity>
    {
        List<Activity> GetList(int tickerId, int numberOfDays);
        List<Activities> RetrieveForId(int id);
        List<CandleStickChart> RetrieveCandleSticks(int id);
    }
}
