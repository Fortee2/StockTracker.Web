using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockTracker.Business.Services.Interfaces;
using StockTracker.Core.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StockTracker.Web.Controllers
{
    public class JobSatusController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class JobStatusController : ControllerBase
        {
            private readonly IJobStatusService _jobService;

            public JobStatusController(IJobStatusService jobStatusService)
            {
                _jobService = jobStatusService;
            }

            // GET: api/jobstatus
            [HttpGet]
            public ActionResult<IEnumerable<JobStatus>> GetJobStatuses()
            {
                return _jobService.RetrieveJobHistory();
            }
        }
    }
}

