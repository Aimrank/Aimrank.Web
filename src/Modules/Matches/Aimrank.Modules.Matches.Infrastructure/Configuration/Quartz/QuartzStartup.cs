using Aimrank.Modules.Matches.Infrastructure.Configuration.Outbox;
using Aimrank.Modules.Matches.Infrastructure.Configuration.Processing;
using Quartz.Impl;
using Quartz;
using System.Collections.Specialized;

namespace Aimrank.Modules.Matches.Infrastructure.Configuration.Quartz
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

            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var processOutboxTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/2 * * ? * *")
                    .Build();

            _scheduler.ScheduleJob(processOutboxJob, processOutboxTrigger).GetAwaiter().GetResult();
        }
    }
}