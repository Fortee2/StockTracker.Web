using System;
using AutoMapper;
using StockTracker.Domain.dto;

namespace StockTracker.Web.Mapping
{
	public class EMADtoMapper:Profile
	{
		public EMADtoMapper()
		{
            CreateMap<EMADto, StockTracker.Core.Domain.EMAData>()
			   .ForMember(dest => dest.ActivityDate, source => source.MapFrom(s=>s.ActivityDate))
			   .ForMember(dest => dest.CalculateValue, source => source.MapFrom(s=> s.CalculateValue))
			   .ForMember(dest => dest.PrevEMA, source => source.MapFrom(s=> s.PreviousEMA))
			   .ReverseMap();
        }
	}
}

