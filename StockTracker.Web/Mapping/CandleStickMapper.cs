using System;
using AutoMapper;
using StockTracker.Domain.DTO;
using StockTracker.Domain.Entities;

namespace StockTracker.Web.Mapping
{
	public class CandleStickMapper:Profile
	{
		public CandleStickMapper()
		{
			CreateMap<Activity, CandleStickChart>()
			   .ForMember(dest => dest.Date, source => source.MapFrom(source => source.ActivityDate))
			   .ReverseMap();
		}


	}
}

