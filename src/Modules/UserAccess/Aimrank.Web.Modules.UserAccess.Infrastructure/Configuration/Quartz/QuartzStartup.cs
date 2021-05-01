using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing.Outbox;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Processing.RemoveProcessedMessages;
using Aimrank.Web.Modules.UserAccess.Infrastructure.Domain.Users.RemoveExpiredTokens;
using Quartz.Impl;
using Quartz;
using System.Collections.Specialized;

namespace Aimrank.Web.Modules.UserAccess.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        private static IScheduler _scheduler;

        internal static void Initialize()
        {
            var schedulerConfiguration = new NameValueCollection
            {
                {"quartz.scheduler.instanceName", "Aimrank.Web.Modules.UserAccess"}
            };

            var schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
            _scheduler.JobFactory = new AutofacJobFactory();
            _scheduler.Start().GetAwaiter().GetResult();
            
            ScheduleCronJob<ProcessOutboxJob>("0/2 * * ? * *");
            ScheduleCronJob<RemoveExpiredTokensJob>("0 0 0 ? * *");
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