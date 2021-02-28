using Aimrank.Application.Contracts;
using MediatR;
using NetArchTest.Rules;
using System.Collections.Generic;
using System.Linq;
using System;
using Xunit;

namespace Aimrank.ArchTests.Application
{
    public class ApplicationTests : TestBase
    {
        [Fact]
        public void Command_Should_Be_Immutable()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(ICommand))
                .Or()
                .ImplementInterface(typeof(ICommand<>))
                .GetTypes();
            
            AssertAreImmutable(types);
        }

        [Fact]
        public void Query_Should_Be_Immutable()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(IQuery<>))
                .GetTypes();
            
            AssertAreImmutable(types);
        }

        [Fact]
        public void Command_Should_Have_Name_EndingWith_Command()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(ICommand))
                .Or()
                .ImplementInterface(typeof(ICommand<>))
                .Should().HaveNameEndingWith("Command")
                .GetResult();
            
            AssertArchTestResult(result);
        }
        
        [Fact]
        public void Query_Should_Have_Name_EndingWith_Query()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(IQuery<>))
                .Should().HaveNameEndingWith("Query")
                .GetResult();
            
            AssertArchTestResult(result);
        }

        [Fact]
        public void CommandHandler_Should_Have_Name_EndingWith_CommandHandler()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .AreClasses()
                .And().ImplementInterface(typeof(ICommandHandler<>))
                .Or().ImplementInterface(typeof(ICommandHandler<,>))
                .Should().HaveNameEndingWith("CommandHandler")
                .GetResult();
            
            AssertArchTestResult(result);
        }
        
        [Fact]
        public void QueryHandler_Should_Have_Name_EndingWith_QueryHandler()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Should().HaveNameEndingWith("QueryHandler")
                .GetResult();
            
            AssertArchTestResult(result);
        }

        [Fact]
        public void Command_And_Query_Handlers_Should_Not_Be_Public()
        {
            var result = Types.InAssembly(ApplicationAssembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<>))
                .Or().ImplementInterface(typeof(ICommandHandler<,>))
                .Or().ImplementInterface(typeof(IQueryHandler<,>))
                .Should().NotBePublic().GetResult();
            
            AssertArchTestResult(result);
        }

        [Fact]
        public void MediatR_RequestHandler_Should_NotBe_Used_Directly()
        {
            var types = Types.InAssembly(ApplicationAssembly)
                .That().DoNotHaveName("ICommandHandler`1")
                .Should().ImplementInterface(typeof(IRequestHandler<>))
                .GetTypes();

            var failingTypes = new List<Type>();

            foreach (var type in types)
            {
                var isCommandHandler = type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    (x.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                     x.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)));
                var isQueryHandler = type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));

                if (!isCommandHandler && !isQueryHandler)
                {
                    failingTypes.Add(type);
                }
            }
            
            AssertFailingTypes(failingTypes);
        }
    }
}