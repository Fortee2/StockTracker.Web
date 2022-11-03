using System;
using System.Collections.Generic;
using AutoMapper;
using StockTracker.Database.investing;
using System.Linq;
using StockTracker.Infrastructure.Repository.Interfaces;
using StockTracker.Domain.Entities;


namespace StockTracker.Infrastructure.Repository
{
	public class SecuritiesRepository : BaseRepository<Ticker>, ISecuritiesRepo
    {
        readonly IMapper _map; 

		public SecuritiesRepository(InvestingContext context, IMapper mapper):base(context)
		{
            _map = mapper;
		}


        public List<Ticker> RetriveveAll()
        {
            var tickers =  from symbols in _dbContext.Tickers
                            select symbols;

            return tickers.ToList();
        }
    }
}

