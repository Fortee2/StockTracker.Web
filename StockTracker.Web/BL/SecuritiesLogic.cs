using System;
using System.Collections.Generic;
using StockTracker.Web.BL.intefaces;
using StockTracker.DAL;
using AutoMapper;
using StockTracker.Database.investing;
using System.Linq;

namespace StockTracker.Web.BL
{
	public class SecuritiesLogic: IBusinessLogic<Securities>
	{
        SecuritiesDAL dAL;
        IMapper map; 

		public SecuritiesLogic(SecuritiesDAL securitiesDAL, IMapper mapper)
		{
            dAL = securitiesDAL;
            map = mapper;
		}

        public void Add(Securities obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Securities obj)
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

        public List<Securities> RetrieveAll()
        {
            var tickers = dAL.GetTickerList();

            return map.Map<List<Ticker>, List<Securities>>(tickers.ToList());
        }

        public void Update(Securities obj)
        {
            throw new NotImplementedException();
        }
    }
}

