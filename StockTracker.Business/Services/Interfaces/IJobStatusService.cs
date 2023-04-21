using System;
using StockTracker.Core.Entities;

namespace StockTracker.Business.Services.Interfaces
{
	public interface IJobStatusService
	{
		List<JobStatus> RetrieveJobHistory();
	}
}

