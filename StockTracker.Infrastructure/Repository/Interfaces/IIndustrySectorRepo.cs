using System;
using StockTracker.Core.Entities;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
    public interface IIndustrySectorRepo : ICRUDRepo<IndustrySector>
    {
        //TODO:  Add code to retrieve all based on job name
        List<IndustrySector> RetrieveAll();
    }
}

