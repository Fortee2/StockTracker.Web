using System;
using System.Collections.Generic;
using StockTracker.DAL;
using AutoMapper;
using StockTracker.Database.investing;
using StockTracker.Domain.DTO;
using System.Linq;
using StockTracker.Web.Repository.intefaces;
using StockTracker.Domain.Entities;
using StockTracker.Web.Repository;

namespace StockTracker.Web.BL
{
	public class SecuritiesRepository : BaseRepository<Ticker>, ISecuritiesRepo
    {
        readonly IMapper _map; 

		public SecuritiesRepository(InvestingContext context, IMapper mapper):base(context)
		{
            _map = mapper;
		}


        public List<Securities> RetriveveAll()
        {
            var tickers =  from symbols in _dbContext.Tickers
                                           select symbols;

            return _map.Map<List<Ticker>, List<Securities>>(tickers.ToList());
        }
    }
}

