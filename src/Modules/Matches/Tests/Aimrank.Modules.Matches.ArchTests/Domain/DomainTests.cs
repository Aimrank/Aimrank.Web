using Aimrank.Common.Domain;
using NetArchTest.Rules;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using Xunit;

namespace Aimrank.Modules.Matches.ArchTests.Domain
{
    public class DomainTests : TestBase
    {
        [Fact]
        public void DomainEvent_Should_Be_Immutable()
        {
            var types = Types.InAssembly(DomainAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEvent))
                .GetTypes();
            
            AssertAreImmutable(types);
        }

        [Fact]
        public void DomainEvent_Should_Have_DomainEvent_Suffix()
        {
            var result = Types.InAssembly(DomainAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEvent))
                .Should().HaveNameEndingWith("DomainEvent")
                .GetResult();
            
            AssertArchTestResult(result);
        }

        [Fact]
        public void ValueObject_Should_Be_Immutable()
        {
            var types = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(ValueObject))
                .GetTypes();
            
            AssertAreImmutable(types);
        }

        [Fact]
        public void Entity_Should_Have_Parameterless_Private_Constructor()
        {
            var entityTypes = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(Entity))
                .GetTypes();

            var failingTypes = new List<Type>();

            foreach (var entityType in entityTypes)
            {
                var constructors = entityType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
                if (constructors.All(c => !(c.IsPrivate && c.GetParameters().Length == 0)))
                {
                    failingTypes.Add(entityType);
                }
            }
            
            AssertFailingTypes(failingTypes);
        }

        [Fact]
        public void BusinessRule_Should_Have_Rule_Suffix()
        {
            var result = Types.InAssembly(DomainAssembly)
                .That()
                .ImplementInterface(typeof(IBusinessRule))
                .Or()
                .ImplementInterface(typeof(IAsyncBusinessRule))
                .Should().HaveNameEndingWith("Rule")
                .GetResult();
            
            AssertArchTestResult(result);
        }
    }
}