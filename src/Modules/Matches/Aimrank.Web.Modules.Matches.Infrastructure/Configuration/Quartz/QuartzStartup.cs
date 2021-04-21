using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Inbox;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Lobbies;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Outbox;
using Quartz.Impl;
using Quartz;
using System.Collections.Specialized;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        private static IScheduler _scheduler;

        internal static void Initialize()
        {
            var schedulerConfiguration = new NameValueCollection
            {
                {"quartz.scheduler.instanceName", "Aimrank.Web.Modules.Matches"}
            };

            var schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
            _scheduler.JobFactory = new AutofacJobFactory();
            _scheduler.Start().GetAwaiter().GetResult();
            
            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var processOutboxTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0/2 * * ? * *")
                .Build();

            _scheduler.ScheduleJob(processOutboxJob, processOutboxTrigger).GetAwaiter().GetResult();

            var processInboxJob = JobBuilder.Create<ProcessInboxJob>().Build();
            var processInboxTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0/5 * * ? * *")
                .Build();
                
            _scheduler.ScheduleJob(processInboxJob, processInboxTrigger).GetAwaiter().GetResult();

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