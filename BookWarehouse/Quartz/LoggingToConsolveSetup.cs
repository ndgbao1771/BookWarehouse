using Microsoft.Extensions.Options;
using Quartz;

namespace BookWarehouse.Quartz
{
    public class LoggingToConsolveSetup : IConfigureOptions<QuartzOptions>
    {
        private readonly IConfiguration _configuration;

        public LoggingToConsolveSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(QuartzOptions options)
        {
            var jobkey = JobKey.Create(nameof(LoggingToConsolve));

            string cronExpression = _configuration.GetSection("Quartz:BookWarehouseJob").Value;

            options.AddJob<LoggingToConsolve>(jobBuilder => jobBuilder.WithIdentity(jobkey))
                   .AddTrigger(trigger => trigger.ForJob(jobkey)
                                                 .WithCronSchedule(cronExpression)
                                                 /*.WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(5)
                                                                                         .RepeatForever())*/);
        }   
    }
}
