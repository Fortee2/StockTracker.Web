using System;
using StockTracker.Infrastructure.Investing;
using StockTracker.Infrastructure.Repository.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace StockTracker.Infrastructure.Repository
{
	public partial class 

        BaseRepository<T>: ICRUDRepo<T> where T : class 	{
        protected InvestingContext _dbContext;
         
		public BaseRepository(InvestingContext context)
		{
            _dbContext = context;
		}

        public void Add(T obj)
        {
            _dbContext.Add(obj);
            _dbContext.SaveChanges();
        }

        public void AddRange(List<T> values)
        {
            _dbContext.AddRange(values);
            _dbContext.SaveChanges();
        }

        public void Delete(T obj)
        {
            _dbContext.Remove(obj);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            T record = FindById(id);
            if(record != null)
            {
                _dbContext.Remove(record);
                _dbContext.SaveChanges();
            }

        }

        public T? FindById(int id)
        {
            return _dbContext.Find<T>(id);
          
        }

        public void Update(T obj)
        {
            _dbContext.Update(obj);
            _dbContext.SaveChanges();
        }

        public InvestingContext GetDbContext()
        {
            return _dbContext;
        }
    }
}

