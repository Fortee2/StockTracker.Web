using System;
using System.Collections.Generic;
using StockTracker.DAL;
using AutoMapper;
using StockTracker.Database.investing;
using StockTracker.Domain.DTO;
using System.Linq;
using StockTracker.Web.Repository.intefaces;
using StockTracker.Domain.Entities;

namespace StockTracker.Web.BL
{
	public class SecuritiesRepository : ISecuritiesRepo

    {
        SecuritiesDAL dAL;
        IMapper map; 

		public SecuritiesRepository(SecuritiesDAL securitiesDAL, IMapper mapper)
		{
            dAL = securitiesDAL;
            map = mapper;
		}

        public void Add(Ticker obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Ticker obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Securities Retrieve(int id)
        {
            throw new NotImplementedException();
        }

        public List<Securities> RetriveveAll()
        {
            var tickers = dAL.GetTickerList();

            return map.Map<List<Ticker>, List<Securities>>(tickers.ToList());
        }

        public void Update(Ticker obj)
        {
            throw new NotImplementedException();
        }
    }
}

