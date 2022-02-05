using System;
using System.Collections.Generic;
using StockTracker.Domain.DTO;

namespace StockTracker.Web.Repository.intefaces
{
    public interface IActivitiesRepo: IRespoitory<Domain.Entities.Activity, Domain.DTO.Activities>
    {
        List<Activities> RetrieveForId(int id);
    }
}
