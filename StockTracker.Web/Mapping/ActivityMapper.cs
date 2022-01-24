using System;
using AutoMapper;

namespace StockTracker.Web.Mapping
{
	public class ActivityMapper:Profile
	{
		public ActivityMapper()
		{
			CreateMap<Database.investing.Activity, dto.Activity>()
			   .ForMember(dest => dest.Date, source => source.MapFrom(source => source.ActivityDate))
			   .ForMember(dest => dest.SecurityId, source => source.MapFrom(source => source.TickerId))
			   .ReverseMap();
		}
	}
}

