using Microsoft.Extensions.Options;
using Quartz;

namespace BookWarehouse.Quartz
{
    public class LoggingToConsoleSetup : IConfigureOptions<QuartzOptions>
    {
        private readonly IConfiguration _configuration;

        public LoggingToConsoleSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(QuartzOptions options)
        {
            var jobkey = JobKey.Create(nameof(LoggingToConsole));

            string cronExpression = _configuration.GetSection("Quartz:BookWarehouseJob").Value;

            options.AddJob<LoggingToConsole>(jobBuilder => jobBuilder.WithIdentity(jobkey))
                   .AddTrigger(trigger => trigger.ForJob(jobkey)
                                                 .WithCronSchedule(cronExpression)
                                                 /*.WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(5)
                                                                                         .RepeatForever())*/);
        }   
    }
}
