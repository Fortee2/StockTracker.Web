using System;
using System.Collections.Generic;

namespace StockTracker.Web.BL.intefaces
{
	public interface IBusinessLogic<T>
	{
		void Add(T obj);
		void Update(T obj);
		void Delete(T obj);
		void Delete(int id);

		List<T> RetrieveAll();
		T Retrieve(int id);

	}
}

