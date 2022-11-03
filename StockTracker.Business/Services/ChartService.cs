using System;
using AutoMapper;
using StockTracker.Business.DTO;
using StockTracker.Business.Services.Interfaces;
using StockTracker.Domain.Entities;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Business.Services
{
	public class ChartService: IChartService
	{
        readonly IMapper _map;
        readonly IActivitiesRepo _repo;

        public ChartService(IMapper autoMapper, IActivitiesRepo activitiesRepo )
		{
            _map = autoMapper;
            _repo = activitiesRepo;
		}


        public List<CandleStickChart> RetrieveCandleSticks(int securityId, int numberOfPeriods)
        {
            return _map.Map<List<Activity>, List<CandleStickChart>>(_repo.GetList(securityId, numberOfPeriods));
        }

        public List<Activities> RetrieveForId(int securityId, int numberOfPeriods)
        {
            return _map.Map<List<Activity>, List<Activities>>(_repo.GetList(securityId, numberOfPeriods));
        }
    }
}

