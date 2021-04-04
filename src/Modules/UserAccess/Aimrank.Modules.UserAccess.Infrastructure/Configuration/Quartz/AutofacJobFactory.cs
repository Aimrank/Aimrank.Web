using Autofac;
using Quartz.Spi;
using Quartz;
using System.Collections.Generic;

namespace Aimrank.Modules.UserAccess.Infrastructure.Configuration.Quartz
{
    internal class AutofacJobFactory : IJobFactory
    {
        private readonly Dictionary<IJob, ILifetimeScope> _scopes = new();
        
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var scope = UserAccessCompositionRoot.BeginLifetimeScope();
            var job = scope.Resolve(bundle.JobDetail.JobType) as IJob;
            _scopes[job] = scope;
            return job;
        }

        public void ReturnJob(IJob job)
        {
            _scopes[job].Dispose();
            _scopes.Remove(job);
        }
    }
}