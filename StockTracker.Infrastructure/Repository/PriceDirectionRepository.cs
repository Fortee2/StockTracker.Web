using System;
using StockTracker.Core.Entities;
using StockTracker.Infrastructure.Investing;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Infrastructure.Repository
{
   public class PriceDirectionRepository: BaseRepository<PriceDirection>, IPriceDirectionRepo
   {
      public PriceDirectionRepository(InvestingContext context):base(context)
      {
      }
   }
}