﻿using System;
using System.Collections.Generic;

namespace StockTracker.Web.Repository.intefaces
{
	public interface IRespoitory<in T, out S>
	{
		void Add(T obj);
		void Update(T obj);
		void Delete(T obj);
		void Delete(int id);

		S Retrieve(int id);
	}
}
