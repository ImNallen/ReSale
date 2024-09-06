using System.Text;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using QuestPDF.Infrastructure;
using ReSale.Application.Abstractions.Authentication;
using ReSale.Application.Abstractions.Caching;
using ReSale.Application.Abstractions.Encryption;
using ReSale.Application.Abstractions.Persistence;
using ReSale.Application.Abstractions.Services;
using ReSale.Domain.Common;
using ReSale.Infrastructure.Authentication;
using ReSale.Infrastructure.Caching;
using ReSale.Infrastructure.Encryption;
using ReSale.Infrastructure.Persistence;
using ReSale.Infrastructure.Services.Email;
using ReSale.Infrastructure.Services.Report;
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
            .AddHealthChecks(configuration)
            .AddFluentEmail(configuration);

    private static IServiceCollection AddFluentEmail(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddFluentEmail(configuration["Email:SenderEmail"], configuration["Email:SenderName"])
            .AddRazorRenderer()
            .AddSmtpSender(configuration["Email:Host"], configuration.GetValue<int>("Email:Port"));

        services.AddScoped<IEmailService, EmailService>();

        return services;
    }

    private static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IReportService, ReportService>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        services.AddSingleton<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(new NpgsqlDataSourceBuilder(connectionString).Build()));

        services.AddDbContext<ReSaleDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default)));

        services.AddScoped<IReSaleDbContext, ReSaleDbContext>();

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
