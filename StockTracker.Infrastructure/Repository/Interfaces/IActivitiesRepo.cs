using System;
using System.Collections.Generic;
using StockTracker.Domain.Entities;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
    public interface IActivitiesRepo:IRespository<Domain.Entities.Activity>
    {
        List<Activity> GetList(int tickerId, int numberOfDays);
    }
}
