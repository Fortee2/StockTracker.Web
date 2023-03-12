using System;
using System.Collections.Generic;
using StockTracker.Infrastructure.Investing;
using StockTracker.Core.Entities;
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

