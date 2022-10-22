using System;
using System.Collections.Generic;
using StockTracker.DAL;
using StockTracker.Web.Repository.intefaces;
using StockTracker.Domain.DTO;
using StockTracker.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using StockTracker.Database.investing;
using StockTracker.Web.Repository;
using Microsoft.Extensions.Options;

namespace StockTracker.Web.BL
{
	public partial class ActivitiesRepository: BaseRepository<Activity>, IActivitiesRepo
	{
        readonly IMapper _map;

        public ActivitiesRepository(InvestingContext investingContext, IMapper mapper):base(investingContext)
		{
            _map = mapper;
		}


        public List<CandleStickChart> RetrieveCandleSticks(int securityId)
        {
            return _map.Map<List<Domain.Entities.Activity>, List<CandleStickChart>>(this.GetList(securityId));
        }

        public List<Activities> RetrieveForId(int securityId)
        {
            return _map.Map<List<Domain.Entities.Activity>, List<Domain.DTO.Activities>>(this.GetList(securityId));
        }

        public List<Activity> GetList(int tickerId, int numberOfDays = 60)
        {
            DateTime start = DateTime.Now.Subtract(new TimeSpan(numberOfDays, 0, 0, 0));


            return (from tradingActivity in _dbContext.Activities
                    where tradingActivity.TickerId == tickerId
                        && tradingActivity.ActivityDate > start
                    orderby tradingActivity.ActivityDate
                    select tradingActivity).ToList();
        }

    
    }
}

