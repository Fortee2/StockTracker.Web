using System;
using System.Collections.Generic;
using AutoMapper;
using StockTracker.Infrastructure.Investing;
using System.Linq;
using StockTracker.Infrastructure.Repository.Interfaces;
using StockTracker.Core.Entities;


namespace StockTracker.Infrastructure.Repository
{
	public class SecuritiesRepository : BaseRepository<Ticker>, ISecuritiesRepo
    {
        readonly IMapper _map; 

		public SecuritiesRepository(InvestingContext context, IMapper mapper):base(context)
		{
            _map = mapper;
		}

        public Ticker? FindBySymbol(string symbol)
        {
            var ticker = (from symbols in _dbContext.Tickers
                         where symbols.Symbol == symbol.ToUpper()
                         select symbols)
                            .FirstOrDefault();

            return ticker;
        }

        public List<Ticker> RetriveveAll()
        {
            var tickers =  from symbols in _dbContext.Tickers
                            select symbols;

            return tickers.ToList();
        }

        public List<Ticker> FindSecurityByIndstry(string industry)
        {
            var tickers = from symbols in _dbContext.Tickers
                          where symbols.Industry.ToUpper() == industry.ToUpper()
                          select symbols;

            return tickers.ToList();
        }
    }
}

