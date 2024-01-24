using Quartz;

namespace BookWarehouse.Quartz
{
    public class LoggingToConsolve : IJob
    {
        private readonly ILogger<LoggingToConsolve> _logger;

        public LoggingToConsolve(ILogger<LoggingToConsolve> logger)
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
