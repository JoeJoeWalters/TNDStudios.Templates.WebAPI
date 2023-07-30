using Microsoft.Extensions.DependencyInjection;
using WebAPI.Workers;

namespace WebAPI.DependencyChecker
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyChecker(this IServiceCollection services, Action<DependencyCheckerConfig> config)
        {
            DependencyWorkerState workerState = new DependencyWorkerState() { LastRan = DateTime.MinValue };
            services.AddSingleton<DependencyWorkerState>(workerState);
            DependencyCheckerConfig configResult = new DependencyCheckerConfig();
            config.Invoke(configResult);
            services.AddSingleton<DependencyCheckerConfig>(configResult);
            services.AddHostedService<DependencyWorker>();

            return services;
        }
    }
}
