using Quartz;

namespace BookWarehouse.Quartz
{
    public class LoggingToConsole : IJob
    {
        private readonly ILogger<LoggingToConsole> _logger;

        public LoggingToConsole(ILogger<LoggingToConsole> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("{UtcNow} - Repeat Task", DateTime.UtcNow);
            return Task.CompletedTask;
        }
    }
}
