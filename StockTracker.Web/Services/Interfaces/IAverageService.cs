using System;
using StockTracker.Domain.dto;
using System.Collections.Generic;
using StockTracker.Core.Domain;
using StockTracker.Core.Interfaces.Calculations;

namespace StockTracker.Web.Services.Interfaces
{
	public interface IAverageService    
	{
        List<MADto> CalculateEMA(List<MADto> eMAs, ushort numberOfPeriods);
        List<IResponse> CalculateMoveingAverage(List<MADto> MAs, ushort numberOfPeriods);
    }
}

