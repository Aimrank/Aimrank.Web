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

            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var processOutboxTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0/2 * * ? * *")
                .Build();

            _scheduler.ScheduleJob(processOutboxJob, processOutboxTrigger).GetAwaiter().GetResult();
            
            var removeProcessedMessagesJob = JobBuilder.Create<RemoveProcessedMessagesJob>().Build();
            var removeProcessedMessagesTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0 0 0/2 ? * *")
                .Build();
                
            _scheduler.ScheduleJob(removeProcessedMessagesJob, removeProcessedMessagesTrigger);

            var removeExpiredTokensJob = JobBuilder.Create<RemoveExpiredTokensJob>().Build();
            var removeExpiredTokensTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0 0 0 ? * *")
                    .Build();

            _scheduler.ScheduleJob(removeExpiredTokensJob, removeExpiredTokensTrigger).GetAwaiter().GetResult();
        }
    }
}