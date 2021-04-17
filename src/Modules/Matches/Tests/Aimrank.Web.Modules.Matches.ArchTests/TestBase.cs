using Aimrank.Web.Modules.Matches.Application.Contracts;
using Aimrank.Web.Modules.Matches.Domain.Matches;
using Aimrank.Web.Modules.Matches.Infrastructure;
using NetArchTest.Rules;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using Xunit;

namespace Aimrank.Web.Modules.Matches.ArchTests
{
    public abstract class TestBase
    {
        protected static Assembly ApplicationAssembly => typeof(IMatchesModule).Assembly;
        protected static Assembly DomainAssembly => typeof(Match).Assembly;
        protected static Assembly InfrastructureAssembly => typeof(MatchesContext).Assembly;

        protected static void AssertAreImmutable(IEnumerable<Type> types)
        {
            var failingTypes = new List<Type>();

            foreach (var type in types)
            {
                if (type.GetFields().Any(x => !x.IsInitOnly) ||
                    type.GetProperties().Any(x => x.CanWrite))
                {
                    failingTypes.Add(type);
                    break;
                }
            }
            
            AssertFailingTypes(failingTypes);
        }

        protected static void AssertFailingTypes(IEnumerable<Type> types)
        {
            Assert.True(types == null || !types.Any());
        }

        protected static void AssertArchTestResult(TestResult result)
        {
            AssertFailingTypes(result.FailingTypes);
        }
    }
}