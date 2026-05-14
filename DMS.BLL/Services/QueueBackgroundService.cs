using DMS.BLL.Interfaces;
using DMS.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DMS.BLL.Services
{
   

    public class QueueBackgroundService
        : BackgroundService
    {
        private readonly
            IServiceScopeFactory
                _scopeFactory;

        public QueueBackgroundService(
            IServiceScopeFactory scopeFactory)
        {
            _scopeFactory =
                scopeFactory;
        }

        protected override async Task
            ExecuteAsync(
                CancellationToken stoppingToken)
        {
            while (!stoppingToken
                .IsCancellationRequested)
            {
                using var scope =
                    _scopeFactory
                        .CreateScope();

                var repository =
                    scope.ServiceProvider
                        .GetRequiredService<
                            IBackgroundJobRepository>();

                var processor =
                    scope.ServiceProvider
                        .GetRequiredService<
                            IJobProcessorService>();

                var jobs =
                    await repository
                        .GetPendingJobsAsync();

                foreach (var job in jobs)
                {
                    await processor
                        .ProcessAsync(job);
                }

                await Task.Delay(
                    5000,
                    stoppingToken);
            }
        }
    }
}
