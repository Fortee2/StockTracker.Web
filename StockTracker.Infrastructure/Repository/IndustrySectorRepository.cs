using System;
using StockTracker.Infrastructure.Investing;
using StockTracker.Infrastructure.Repository.Interfaces;
using System.Linq;
using System.Collections.Generic;
using StockTracker.Core.Entities;

namespace StockTracker.Infrastructure.Repository
{
    public partial class IndustrySectorRepository : BaseRepository<IndustrySector>, IIndustrySectorRepo
    {
        public IndustrySectorRepository(InvestingContext context):base(context)
        {
        }

        public List<IndustrySector> RetrieveAll()
        {
            var sectors = from sector in _dbContext.IndustrySectors
                          select sector;

            return sectors.ToList();
        }
    }
}