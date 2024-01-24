using Quartz;

namespace BookWarehouse.Quartz
{
    public static class QuartzDI
    {
        public static void AddInfratructure(this IServiceCollection services)
        {
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();

                
            });

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            services.ConfigureOptions<LoggingToConsolveSetup>();
        }
    }
}
