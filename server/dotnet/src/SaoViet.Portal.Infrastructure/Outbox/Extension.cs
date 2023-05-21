using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace SaoViet.Portal.Infrastructure.Outbox;

public static class Extension
{
    public static void AddTransientOutbox(this IServiceCollection services)
    {
        services.AddSingleton<OutboxInterceptor>();
        services.AddQuartz(cfg =>
        {
            var jobKey = new JobKey(nameof(OutboxInterceptor));
            cfg.AddJob<OutboxJob>(jobKey)
                .AddTrigger(trg => trg
                    .WithIdentity($"{nameof(OutboxJob)}_trigger")
                    .ForJob(jobKey)
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .WithDescription($"{nameof(OutboxJob)}_trigger"));

            cfg.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    }
}