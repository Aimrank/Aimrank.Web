using Quartz.Impl;
using Quartz;
using System.Collections.Specialized;
using Aimrank.Modules.UserAccess.Infrastructure.Domain.Users.RemoveExpiredTokens;

namespace Aimrank.Modules.UserAccess.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        private static IScheduler _scheduler;

        internal static void Initialize()
        {
            var schedulerConfiguration = new NameValueCollection
            {
                {"quartz.scheduler.instanceName", "Aimrank.Modules.UserAccess"}
            };

            var schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();
            _scheduler.JobFactory = new AutofacJobFactory();
            _scheduler.Start().GetAwaiter().GetResult();

            var removeExpiredTokensJob = JobBuilder.Create<RemoveExpiredTokensJob>().Build();
            var removeExpiredTokensTrigger =
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0 0 0 * * *")
                    .Build();

            _scheduler.ScheduleJob(removeExpiredTokensJob, removeExpiredTokensTrigger).GetAwaiter().GetResult();
        }
    }
}