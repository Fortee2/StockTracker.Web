using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockTracker.Business.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

public class MovingAveragesBackgroundService : IHostedService, IDisposable
{
    private readonly IServiceProvider _services;
    private Timer _timer;

    public MovingAveragesBackgroundService(IServiceProvider services)
    {
        _services = services;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Calculate moving averages on app startup
        CalculateMovingAverages();

        // Schedule the task to run every 24 hours
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        // Stop the timer
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        // Dispose the timer
        _timer?.Dispose();
    }

    private void DoWork(object state)
    {
        CalculateMovingAverages();
    }

    private void CalculateMovingAverages()
    {
        using (var scope = _services.CreateScope())
        {
            var securitiesService = scope.ServiceProvider.GetRequiredService<ISecuritiesService>();
            var averageService = scope.ServiceProvider.GetRequiredService<IAverageService>();
            var tickers = securitiesService.RetriveveAll();
            averageService.CalculateAllAverages(tickers);
        }
    }
}
