using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Portal.Shared.Core.Helpers;
using Serilog;

namespace Portal.Shared.Infrastructure.Persistence;

public abstract class ApplicationDbContextDesignFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
{
    private readonly string _dbName;

    protected ApplicationDbContextDesignFactoryBase(string dbName) => _dbName = dbName;

    public TContext CreateDbContext(string[] args)
    {
        var configuration = ConfigurationHelper
            .GetConfiguration(AppContext.BaseDirectory)
            .GetConnectionString(_dbName);

        var optionsBuilder = new DbContextOptionsBuilder<TContext>()
            .UseSqlServer(configuration ?? throw new InvalidOperationException(),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                })
            .UseLoggerFactory(LoggerFactory.Create(log =>
            {
                log.AddConsole();
                log.AddSerilog(dispose: true);
            }))
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

        return (Activator.CreateInstance(typeof(TContext), optionsBuilder.Options) as TContext)!;
    }
}