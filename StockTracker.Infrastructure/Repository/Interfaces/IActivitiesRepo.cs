using System;
using System.Collections.Generic;
using StockTracker.Core.Entities;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
    public interface IActivitiesRepo:IRespository<Activity>
    {
        List<Activity> GetList(int tickerId, int numberOfDays);
    }
}
