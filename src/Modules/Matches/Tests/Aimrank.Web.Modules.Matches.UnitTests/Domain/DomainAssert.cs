using Aimrank.Web.Common.Domain;
using System.Linq;
using System.Threading.Tasks;
using System;
using Xunit;

namespace Aimrank.Web.Modules.Matches.UnitTests.Domain
{
    public static class DomainAssert
    {
        public static void FailsBusinessRule<T>(Action testCode) where T : IBusinessRuleBase
        {
            try
            {
                testCode();
            }
            catch (BusinessRuleValidationException exception)
            {
                Assert.IsType<T>(exception.BrokenRule);
            }
        }
        
        public static async Task FailsBusinessRuleAsync<T>(Func<Task> testCode) where T : IBusinessRuleBase
        {
            try
            {
                await testCode();
            }
            catch (BusinessRuleValidationException exception)
            {
                Assert.IsType<T>(exception.BrokenRule);
            }
        }

        public static void ShouldEmitDomainEvent<T>(this Entity entity, Func<object, bool> conditions = null) where T : IDomainEvent
        {
            Assert.NotNull(entity);

            var domainEvent = entity.DomainEvents.FirstOrDefault(e => e.GetType() == typeof(T));
            
            Assert.NotNull(domainEvent);

            if (conditions is not null && domainEvent is not null)
            {
                Assert.True(conditions((IDomainEvent) domainEvent));
            }
        }
    }
}