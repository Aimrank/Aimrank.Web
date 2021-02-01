using Aimrank.Infrastructure.Configuration.Quartz.Jobs;
using Quartz.Impl;
using Quartz;
using System.Collections.Specialized;

namespace Aimrank.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        private static IScheduler _scheduler;

        internal static void Initialize()
        {
            var schedulerConfiguration = new NameValueCollection
            {
                {"quartz.scheduler.instanceName", "Aimrank"}
            };

            var schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
            _scheduler.JobFactory = new AutofacJobFactory();
            _scheduler.Start().GetAwaiter().GetResult();

            var processLobbiesJob = JobBuilder.Create<ProcessLobbiesJob>().Build();
            var processLobbiesTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/5 * * ? * *")
                    .Build();

            _scheduler.ScheduleJob(processLobbiesJob, processLobbiesTrigger).GetAwaiter().GetResult();
        }
    }
}