using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using Quartz;
using System.Collections.Generic;

namespace Aimrank.Web.Modules.Matches.Infrastructure.Configuration.Quartz
{
    internal class CompositionRootJobFactory : IJobFactory
    {
        private readonly Dictionary<IJob, IServiceScope> _scopes = new();
        
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var scope = MatchesCompositionRoot.CreateScope();
            var job = scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
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