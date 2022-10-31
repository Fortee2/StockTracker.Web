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

		public List<MADto> CalculateEMA(List<MADto> eMAs, ushort numberOfPeriods)
		{
			if (eMAs.Count == 0) return new List<MADto>();

			List<ITradingStructure> eMAData = new();
			eMAData.AddRange(_mapper.Map<List<MADto>, List<MAData>>(eMAs));

			ExponetialMovingAverage exponetialMovingAverage = new Core.Calculations.ExponetialMovingAverage(eMAData);

			exponetialMovingAverage.NumberOfPeriods = numberOfPeriods;
			exponetialMovingAverage.ColumnPreviousEma = "PrevMA";
			exponetialMovingAverage.ColumnToAvg = "CalculateValue";

            var resp =  exponetialMovingAverage.Calculate();

			List<MADto> dtos = new List<MADto>();

			foreach(IResponse response in resp)
			{
				dtos.Add(new MADto(response.ActivityDate, 0,response.GetDecimalValue("Value")));
			}

			return dtos;
        }

		public List<IResponse> CalculateMoveingAverage(List<MADto> MAs, ushort numberOfPeriods)
		{
            if (MAs.Count == 0) return new List<IResponse>();

            List<ITradingStructure> mAData = new();
            mAData.AddRange(_mapper.Map<List<MADto>, List<MAData>>(MAs));

            MovingAveraage movingAveraage = new MovingAveraage((IList<ITradingStructure>)mAData);

			movingAveraage.NumberOfPeriods = numberOfPeriods;
            movingAveraage.ColumnToAvg = "CalculateValue";

            return movingAveraage.Calculate();
		}
	}
}

