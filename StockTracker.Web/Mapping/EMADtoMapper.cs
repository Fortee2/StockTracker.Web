using System;
using AutoMapper;
using StockTracker.Domain.dto;

namespace StockTracker.Web.Mapping
{
	public class EMADtoMapper:Profile
	{
		public EMADtoMapper()
		{
            CreateMap<MADto, StockTracker.Core.Domain.MAData>()
			   .ForMember(dest => dest.ActivityDate, source => source.MapFrom(s=>s.ActivityDate))
			   .ForMember(dest => dest.CalculateValue, source => source.MapFrom(s=> s.CalculateValue))
			   .ForMember(dest => dest.PrevMA, source => source.MapFrom(s=> s.PreviousMA))
			   .ReverseMap();
        }
	}
}

