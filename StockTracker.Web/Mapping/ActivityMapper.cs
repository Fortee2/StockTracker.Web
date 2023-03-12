using System;
using AutoMapper;
using StockTracker.Core.Entities;
using StockTracker.Core;

namespace StockTracker.Web.Mapping
{
	public class ActivityMapper:Profile
	{
		public ActivityMapper()
		{
			CreateMap<Activity,Business.DTO.Activities>()
			   .ForMember(dest => dest.Date, source => source.MapFrom(source => source.ActivityDate))
			   .ForMember(dest => dest.SecurityId, source => source.MapFrom(source => source.TickerId))
			   .ReverseMap();
		}
	}
}

