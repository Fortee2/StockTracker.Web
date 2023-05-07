using System;
using AutoMapper;
using StockTracker.Core.Entities;
using StockTracker.Infrastructure.Investing;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Infrastructure.Repository
{
	public class RelativeStrengthRepository:BaseRepository<Rsi>, IRsiRepo
	{
		IMapper _map;

		public RelativeStrengthRepository(InvestingContext context, IMapper mapper) : base(context)
        {
            _map = mapper;
        }
	}
}

