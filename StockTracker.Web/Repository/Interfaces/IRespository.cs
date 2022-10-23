using System;
using System.Collections.Generic;

namespace StockTracker.Web.Repository.Interfaces
{
	public interface IRespoitory<T>
	{
		void Add(T obj);
		void Update(T obj);
		void Delete(T obj);
		void Delete(int id);

		T FindById(int Id);
	}
}

