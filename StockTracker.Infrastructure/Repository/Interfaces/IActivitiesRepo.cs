using System;
using System.Collections.Generic;
using StockTracker.Domain.Entities;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
    public interface IActivitiesRepo:IRespoitory<Domain.Entities.Activity>
    {
        List<Activity> GetList(int tickerId, int numberOfDays);
    }
}
