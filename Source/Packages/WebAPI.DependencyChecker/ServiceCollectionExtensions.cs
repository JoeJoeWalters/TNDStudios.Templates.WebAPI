using Microsoft.Extensions.DependencyInjection;
using WebAPI.Workers;

namespace WebAPI.DependencyChecker
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyChecker(this IServiceCollection services, DependencyCheckerConfig config)
        {
            DependencyWorkerState workerState = new DependencyWorkerState() { LastRan = DateTime.MinValue };
            services.AddSingleton<DependencyWorkerState>(workerState);
            services.AddSingleton<DependencyCheckerConfig>(config);
            services.AddHostedService<DependencyWorker>();

            return services;
        }
    }
}
