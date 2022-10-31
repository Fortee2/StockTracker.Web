using System;
using System.Collections.Generic;
using StockTracker.Domain.dto;
using StockTracker.Domain.Enumerations;

namespace StockTracker.Web.Repository.Interfaces
{
	public interface IAveragesRepo: IRespoitory<Domain.Entities.Averages>
    {
        MADto RetrieveLastAverage(int tickerId, AverageTypes averageType);
        List<MADto> RetrieveDataForPriceCalculations(int tickerId, AverageTypes averageType);

    }


}

