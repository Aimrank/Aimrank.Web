using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Inbox;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Lobbies;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.Outbox;
using Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Processing.RemoveProcessedMessages;
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
            
            ScheduleCronJob<ProcessOutboxJob>("0/2 * * ? * *");
            ScheduleCronJob<ProcessInboxJob>("0/5 * * ? * *");
            ScheduleCronJob<ProcessLobbiesJob>("0/5 * * ? * *");
            ScheduleCronJob<RemoveProcessedMessagesJob>("0 0 0/2 ? * *");
        }
        
        private static void ScheduleCronJob<T>(string cron) where T : class, IJob
        {
            var job = JobBuilder.Create<T>().Build();
            var trigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule(cron)
                .Build();

            _scheduler.ScheduleJob(job, trigger);
        }
    }
}