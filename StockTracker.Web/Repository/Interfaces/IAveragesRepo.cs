using System;
using System.Collections.Generic;
using StockTracker.Domain.dto;
using StockTracker.Domain.Enumerations;

namespace StockTracker.Web.Repository.Interfaces
{
	public interface IAveragesRepo: IRespoitory<Domain.Entities.Averages>
    {
        EMADto RetrieveLastEMA(int tickerId, AverageTypes averageType);
        List<EMADto> RetrieveDataForPriceCalculations(int tickerId, AverageTypes averageType);

    }


}

