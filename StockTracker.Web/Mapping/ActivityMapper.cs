using System;
using AutoMapper;
using StockTracker.Domain.Entities;
using StockTracker.Domain;

namespace StockTracker.Web.Mapping
{
	public class ActivityMapper:Profile
	{
		public ActivityMapper()
		{
			CreateMap<Activity,Domain.DTO.Activities>()
			   .ForMember(dest => dest.Date, source => source.MapFrom(source => source.ActivityDate))
			   .ForMember(dest => dest.SecurityId, source => source.MapFrom(source => source.TickerId))
			   .ReverseMap();
		}
	}
}

