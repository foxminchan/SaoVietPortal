using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using SaoViet.Portal.Domain.Interfaces;

namespace SaoViet.Portal.Infrastructure.Persistence;

public static class Extension
{
    public static void AddSqlServiceCollection(this IServiceCollection services,
        string connString,
        Action<DbContextOptionsBuilder>? optionsAction = null,
        Action<IServiceCollection>? serviceAction = null,
        ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
            });
            options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
            optionsAction?.Invoke(options);
        }, serviceLifetime);
        services.AddScoped<IDatabaseFacade>(p => p.GetRequiredService<ApplicationDbContext>());
        serviceAction?.Invoke(services);
    }

    public static IServiceCollection AddRepository(this IServiceCollection services, Type repoType)
        => services.Scan(scan => scan
            .FromAssembliesOf(repoType)
            .AddClasses(classes =>
                classes.AssignableTo(repoType)).As(typeof(IRepository<>)).WithScopedLifetime());

    public static void MigrateDataFromScript(this MigrationBuilder migrationBuilder)
    {
        var assembly = Assembly.GetCallingAssembly();
        var files = assembly.GetManifestResourceNames();
        var filePrefix = $"{assembly.GetName().Name}.Data.Scripts.";
        foreach (var file in files
                     .Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
                     .Select(f => new { PhysicalFile = f, LogicalFile = f.Replace(filePrefix, string.Empty) })
                     .OrderBy(f => f.LogicalFile))
        {
            using var stream = assembly.GetManifestResourceStream(file.PhysicalFile);
            using var reader = new StreamReader(stream!);
            var command = reader.ReadToEnd();
            if (string.IsNullOrWhiteSpace(command))
                continue;
            migrationBuilder.Sql(command);
        }
    }

    public static async Task DoDbMigrationAsync(this IApplicationBuilder app, ILogger logger)
    {
        var scope = app.ApplicationServices.CreateAsyncScope();
        var dbFacadeResolver = scope.ServiceProvider.GetService<IDatabaseFacade>();
        var policy = CreatePolicy(3, logger, nameof(WebApplication));
        await policy.ExecuteAsync(async () =>
        {
            if (!await dbFacadeResolver?.Database.CanConnectAsync()!)
            {
                logger.LogError("Connection String: {conn}",
                    dbFacadeResolver.Database.GetConnectionString());
                throw new Exception("Can not connect to database");
            }

            var migrations = await dbFacadeResolver.Database.GetPendingMigrationsAsync();
            if (migrations.Any())
            {
                await dbFacadeResolver.Database.MigrateAsync();
                logger.LogInformation("Migrated is done");
            }
        });

        static AsyncRetryPolicy CreatePolicy(int retries, ILogger logger, string prefix)
        {
            return Policy.Handle<Exception>().WaitAndRetryAsync(
                retries,
                _ => TimeSpan.FromSeconds(15),
                (exception, _, retry, _) =>
                {
                    logger.LogWarning(exception,
                        "[{Prefix}] Exception {ExceptionType} with message {Message} detected on attempt {Retry} of {Retries}",
                        prefix, exception.GetType().Name, exception.Message, retry, retries);
                }
            );
        }
    }
}