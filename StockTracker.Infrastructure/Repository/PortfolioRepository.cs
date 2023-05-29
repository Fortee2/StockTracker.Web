using StockTracker.Core.Entities;
using StockTracker.Infrastructure.Investing;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Infrastructure.Repository
{
	public class PortfolioRepository:BaseRepository<Portfolio>, IPortfolioRepo
	{
		public PortfolioRepository(InvestingContext context):base(context)
		{
		}

        public List<Portfolio> RetrieveAllActive()
        {
            var statuses = (from portfolioItems in _dbContext.Portfolios
                            where portfolioItems.Active == true
                           orderby portfolioItems.DateAdded descending
                           select portfolioItems);

            return statuses.ToList();
        }
    }
}