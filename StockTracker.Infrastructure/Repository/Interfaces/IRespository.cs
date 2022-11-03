using System;
using System.Collections.Generic;
using StockTracker.Database.investing;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
	public interface IRespoitory<T>
	{
		void Add(T obj);
		void Update(T obj);
		void Delete(T obj);
		void Delete(int id);
		void AddRange(List<T> values);

        InvestingContext GetDbContext();

		T FindById(int Id);
	}
}

