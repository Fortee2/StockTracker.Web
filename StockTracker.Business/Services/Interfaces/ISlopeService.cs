using System;
using StockTracker.Business.dto;
using StockTracker.Business.DTO;
using StockTracker.Business.Enumerations;
using StockTracker.Core.Domain;
using StockTracker.Core.Entities;

namespace StockTracker.Business.Services.Interfaces
{
	public interface ISlopeService{
        SlopeDto CalculateSlope(AverageTypes averageType, int tickerId, DateTime tradeDate, int numberOfPeriods);
        void SaveSlope(SlopeDto slopeDto, int tickerId);
    }
}