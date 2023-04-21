using System;
using AutoMapper;
using StockTracker.Business.DTO;
using StockTracker.Business.Services.Interfaces;
using StockTracker.Core.Entities;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Business.Services
{
	public class SecuritiesService:ISecuritiesService
	{
		private readonly ISecuritiesRepo _repo;
		private readonly IMapper _map;

        public SecuritiesService(ISecuritiesRepo securitiesRepo, IMapper mapper)
		{
			_repo = securitiesRepo;
			_map = mapper;
        }

		public List<Securities> RetriveveAll()
		{
            var tickerList = _repo.RetriveveAll();

			return _map.Map<IList<Ticker>, List<Securities>>(tickerList);
        }

		public Securities? FindSecurityBySymbol(string symbol)
		{
			if(symbol.Trim() == "")
			{
				throw new Exception("Security Symbol can not be blank.");
			}

			var security = _repo.FindBySymbol(symbol);

            return (security == null)? null : _map.Map<Ticker, Securities>(security);
		}
	}
}

