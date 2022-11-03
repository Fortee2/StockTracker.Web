using System;
using StockTracker.Business.DTO;

namespace StockTracker.Business.Services.Interfaces
{
	public interface ISecuritiesService
	{
        List<Securities> RetriveveAll();

    }
}

