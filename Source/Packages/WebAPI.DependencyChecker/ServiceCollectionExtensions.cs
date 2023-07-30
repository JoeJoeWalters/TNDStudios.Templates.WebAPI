using Microsoft.Extensions.DependencyInjection;
using WebAPI.Workers;

namespace WebAPI.DependencyChecker
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyChecker(this IServiceCollection services, Action<DependencyCheckerConfig> config)
        {
            // Report on the state of the worker (last run etc.)
            DependencyWorkerState workerState = new DependencyWorkerState() { LastRan = DateTime.MinValue };
            services.AddSingleton<DependencyWorkerState>(workerState); 

            // Set up the configuration
            DependencyCheckerConfig configResult = new DependencyCheckerConfig(); // Bring up the defaults
            config.Invoke(configResult); // Apply overrides from caller
            services.AddSingleton<DependencyCheckerConfig>(configResult); // Set the config result of the default + overrides

            // Kick off the hosted services to check the dependencies
            services.AddHostedService<DependencyWorker>(); 

            // Pass to the next on the services chain (if needed)
            return services;
        }
    }
}
