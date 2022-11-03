﻿using System;
using System.Collections.Generic;
using StockTracker.Core.Domain;
using StockTracker.Core.Interfaces.Calculations;
using StockTracker.Business.DTO;
using StockTracker.Business.Enumerations;

namespace StockTracker.Business.Services.Interfaces
{
	public interface IAverageService    
	{
        List<MADto> CalculateEMA(List<MADto> eMAs, ushort numberOfPeriods);
        List<IResponse> CalculateMoveingAverage(List<MADto> MAs, ushort numberOfPeriods);
        void CalculateAllAverages(List<Securities> tickers);

        MADto? RetrieveLastAverage(int tickerId, AverageTypes averageType);
        List<MADto> RetrieveDataForAverageCalculations(int tickerId, AverageTypes averageType, ushort numberOfPeriods);
    }
}

