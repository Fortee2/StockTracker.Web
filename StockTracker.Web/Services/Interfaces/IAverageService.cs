using System;
using StockTracker.Domain.dto;
using System.Collections.Generic;

namespace StockTracker.Web.Services.Interfaces
{
	public interface IAverageService    
	{
        List<EMADto> CalculateEMA(List<EMADto> eMAs);

    }
}

