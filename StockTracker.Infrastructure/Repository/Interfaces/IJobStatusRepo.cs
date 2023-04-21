using System;
using StockTracker.Core.Entities;

namespace StockTracker.Infrastructure.Repository.Interfaces
{
	public interface IJobStatusRepo : ICRUDRepo<JobStatus>
	{
        //TODO:  Add code to retrieve all based on job name
        List<JobStatus> RetrieveAll();
    }
}

