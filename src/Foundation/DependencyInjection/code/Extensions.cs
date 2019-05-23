using System;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
namespace EMAAR.Emaarcommunities.Foundation.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSitecoreScoped<TService, TImplementation>(this IServiceCollection services)
          where TService : class where TImplementation : class, TService
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            return services.AddScoped<TService, TImplementation>()
              .AddSingletonThunk<TService>();
        }

        public static IServiceCollection AddSitecoreScoped<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
          where TService : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (implementationFactory == null) throw new ArgumentNullException(nameof(implementationFactory));
            return services.AddScoped(implementationFactory)
              .AddSingletonThunk<TService>();
        }

        private static IServiceCollection AddSingletonThunk<TService>(this IServiceCollection services)
          where TService : class
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddSingleton<Func<TService>>(_ => () => ServiceLocator.ServiceProvider.GetService<TService>());
            return services;
        }
    }
}