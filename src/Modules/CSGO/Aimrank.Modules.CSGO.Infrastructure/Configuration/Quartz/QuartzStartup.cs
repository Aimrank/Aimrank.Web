using Aimrank.Modules.CSGO.Infrastructure.Application;
using Quartz.Impl;
using Quartz;
using System.Collections.Specialized;

namespace Aimrank.Modules.CSGO.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        private static IScheduler _scheduler;

        internal static void Initialize()
        {
            var schedulerConfiguration = new NameValueCollection
            {
                {"quartz.scheduler.instanceName", "Aimrank.Modules.CSGO"}
            };

            var schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
            _scheduler.JobFactory = new AutofacJobFactory();
            _scheduler.Start().GetAwaiter().GetResult();
            
            var removeInactivePodsJob = JobBuilder.Create<RemoveInactivePodsJob>().Build();
            var removeInactivePodsTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/30 * * ? * *")
                    .Build();

            _scheduler.ScheduleJob(removeInactivePodsJob, removeInactivePodsTrigger).GetAwaiter().GetResult();
        }
    }
}