using System;
using AutoMapper;
using StockTracker.Database.investing;

namespace StockTracker.Web.Mapping
{
    public class SecuritiesMapper:Profile
    {
        public SecuritiesMapper()
        {
            CreateMap<Ticker, Securities>()
                .ForMember(dest => dest.Name, source => source.MapFrom(source => source.TickerName))
                .ReverseMap();
        }
    }
}
