using Aimrank.Web.Modules.Cluster.Infrastructure.Application;
using Quartz.Impl;
using Quartz;
using System.Collections.Specialized;

namespace Aimrank.Web.Modules.Cluster.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        private static IScheduler _scheduler;

        internal static void Initialize()
        {
            var schedulerConfiguration = new NameValueCollection
            {
                {"quartz.scheduler.instanceName", "Aimrank.Web.Modules.Cluster"}
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