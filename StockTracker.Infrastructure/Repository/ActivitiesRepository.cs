using System;
using System.Collections.Generic;
using StockTracker.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using StockTracker.Database.investing;
using StockTracker.Infrastructure.Repository;
using Microsoft.Extensions.Options;
using System.Collections;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Infrastructure.Repository
{
	public partial class ActivitiesRepository: BaseRepository<Activity>, IActivitiesRepo
	{
        readonly IMapper _map;

        public ActivitiesRepository(InvestingContext investingContext, IMapper mapper):base(investingContext)
		{
            _map = mapper;
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

        public List<Activity> GetActivitiesFrom(int tickerId, DateTime lastDate)
        {
            return (from tradingActivity in _dbContext.Activities
                    where tradingActivity.TickerId == tickerId
                        && tradingActivity.ActivityDate > lastDate
                    orderby tradingActivity.ActivityDate 
                    select tradingActivity).ToList();
        }
    }
}

