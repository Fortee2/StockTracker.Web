using System;
using StockTracker.Core.Entities;
using StockTracker.Infrastructure.Investing;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Infrastructure.Repository
{
	public class JobStatusRepository:BaseRepository<JobStatus>, IJobStatusRepo
	{
		public JobStatusRepository(InvestingContext context):base(context)
		{
		}
	}
}

