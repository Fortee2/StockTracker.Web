using System;
using StockTracker.Business.Services.Interfaces;
using StockTracker.Core.Entities;
using StockTracker.Infrastructure.Repository;
using StockTracker.Infrastructure.Repository.Interfaces;

namespace StockTracker.Business.Services
{
	public class JobStatusService:IJobStatusService
	{
		IJobStatusRepo _jobStatus;

		public JobStatusService(IJobStatusRepo jobStatusRepository)
		{
			_jobStatus = jobStatusRepository;
		}

        public List<JobStatus> RetrieveJobHistory()
        {
			return _jobStatus.RetrieveAll();
        }
    }
}

