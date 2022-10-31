using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using StockTracker.Domain.Entities;
using StockTracker.Domain.Enumerations;
using StockTracker.Web.Repository.Interfaces;
using StockTracker.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;

namespace StockTracker.Web.Services
{
    public class AveragesHostedService : BackgroundService
    {
        private IAverageService _averageService;



        public AveragesHostedService(IAverageService averageService, IAveragesRepo averageRepo, ISecuritiesRepo securitiesRepo)
        {
            _averageService = averageService;
            _repo = averageRepo;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

        }

        private void SubmitAverage(Averages average)
        {
            HttpClient client = new HttpClient();

            client.PostAsJsonAsync("https://localhost:5001/api/averages/Add", average);
        }
        

    }
}

