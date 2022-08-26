using Application.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Config;
using Persistence.Repositories;
using Redis.OM;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new RedisConnectionProvider(Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING", EnvironmentVariableTarget.Process)));
        services.AddHostedService<IndexCreationService>();
        
        services.AddSingleton<IUnitOfWork, UnitOfWork>();

        return services;
    }
}