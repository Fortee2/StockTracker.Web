using System;
using System.Collections.Generic;
using AutoMapper;
using StockTracker.Core.Calculations;
using StockTracker.Core.Calculations.Response;
using StockTracker.Core.Domain;
using StockTracker.Core.Interfaces.Calculations;
using StockTracker.Core.Interfaces;
using StockTracker.Domain.dto;
using StockTracker.Domain.Entities;
using StockTracker.Domain.Enumerations;
using StockTracker.Web.Repository.Interfaces;
using Averages = StockTracker.Domain.Entities.Averages;
using StockTracker.Web.Services.Interfaces;

namespace StockTracker.Web
{
	public class AveragesService: IAverageService
    {
		IAveragesRepo _repo;
		IMapper _mapper;

		public AveragesService(IAveragesRepo activitiesRepo, IMapper mapper)
		{
			_repo = activitiesRepo;
			_mapper = mapper;
		}

		public List<EMADto> CalculateEMA(List<EMADto> eMAs)
		{
			if (eMAs.Count == 0) return new List<EMADto>();

			List<EMAData> eMAData = _mapper.Map<List<EMADto>, List<EMAData>>(eMAs);

			ExponetialMovingAverage exponetialMovingAverage = new Core.Calculations.ExponetialMovingAverage((IList<ITradingStructure>)eMAData);

			exponetialMovingAverage.NumberOfPeriods = 12;
			exponetialMovingAverage.ColumnPreviousEma = "PrevEMA";
			exponetialMovingAverage.ColumnToAvg = "CalculateValue";

            var resp =  exponetialMovingAverage.Calculate();

			List<EMADto> dtos = new List<EMADto>();

			foreach(IResponse response in resp)
			{
				dtos.Add(new EMADto(response.ActivityDate, 0,response.GetDecimalValue("Value")));
			}

			return dtos;
        }
	}
}

