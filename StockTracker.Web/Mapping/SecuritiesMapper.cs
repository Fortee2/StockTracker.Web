using System;
using AutoMapper;
using StockTracker.Infrastructure.Investing;
using StockTracker.Core.Entities;
using StockTracker.Business.DTO;

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
