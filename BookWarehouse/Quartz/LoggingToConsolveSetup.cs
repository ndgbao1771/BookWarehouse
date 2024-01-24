using Microsoft.Extensions.Options;
using Quartz;

namespace BookWarehouse.Quartz
{
    public class LoggingToConsolveSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobkey = JobKey.Create(nameof(LoggingToConsolve));

            options.AddJob<LoggingToConsolve>(jobBuilder => jobBuilder.WithIdentity(jobkey))
                   .AddTrigger(trigger => trigger.ForJob(jobkey)
                                                 .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(5)
                                                                                         .RepeatForever()));
        }   
    }
}
