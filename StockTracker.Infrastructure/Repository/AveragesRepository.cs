using System;
using System.Collections.Generic;
using StockTracker.Database.investing;
using StockTracker.Domain.Entities;
using System.Linq;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Infrastructure.Repository
{
	public partial class AveragesRepository:BaseRepository<Averages>, IAveragesRepo
    {
		public AveragesRepository(InvestingContext investingContext):base(investingContext)
		{
		}


    }
}

