using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Npgsql;
using ReSale.Application.Abstractions.Authentication;
using ReSale.Application.Abstractions.Caching;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Abstractions.Persistence.Repositories;
using ReSale.Domain.Common;
using ReSale.Infrastructure.Authentication;
using ReSale.Infrastructure.Caching;
using ReSale.Infrastructure.Persistence;
using ReSale.Infrastructure.Persistence.Repositories;
using ReSale.Infrastructure.Time;

namespace ReSale.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddServices()
            .AddDatabase(configuration)
            .AddCaching(configuration)
            .AddAuthentication(configuration)
            .AddHealthChecks(configuration);

    private static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        
        services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

        services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));
        
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddTransient<AdminAuthorizationDelegatingHandler>();
        
        services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
        {
            var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            
            httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
        })
        .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

        services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpclient) =>
        {
            var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;
            
            httpclient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
        });
        
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
    
    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        var connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(new NpgsqlDataSourceBuilder(connectionString).Build()));

        services.AddDbContext<ReSaleDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default)));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ReSaleDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
    
    private static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        string redisConnectionString = configuration.GetConnectionString("Cache")!;

        services.AddStackExchangeRedisCache(options => options.Configuration = redisConnectionString);

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
    
    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!)
            .AddRedis(configuration.GetConnectionString("Cache")!);

        return services;
    }
}