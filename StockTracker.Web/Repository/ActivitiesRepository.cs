using System;
using System.Collections.Generic;
using StockTracker.DAL;
using StockTracker.Web.Repository.intefaces;
using StockTracker.Domain.DTO;
using StockTracker.Domain.Entities;
using AutoMapper;

namespace StockTracker.Web.BL
{
	public class ActivitiesRepository:IActivitiesRepo
	{
        private ActivitiesDAL dal;
        IMapper map;

        public ActivitiesRepository(ActivitiesDAL activitiesDal, IMapper mapper)
		{
            dal = activitiesDal;
            map = mapper;
		}

        public void Add(Activity obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Activity obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Activities Retrieve(int id)
        {
            throw new NotImplementedException();
        }

        public List<CandleStickChart> RetrieveCandleSticks(int securityId)
        {
            return map.Map<List<Domain.Entities.Activity>, List<CandleStickChart>>(dal.GetList(securityId));
        }

        public List<Activities> RetrieveForId(int securityId)
        {
            return map.Map<List<Domain.Entities.Activity>, List<Domain.DTO.Activities>>(dal.GetList(securityId));
        }

        public void Update(Activity obj)
        {
            throw new NotImplementedException();
        }
    }
}

