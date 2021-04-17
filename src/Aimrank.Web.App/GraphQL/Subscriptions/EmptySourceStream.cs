using HotChocolate.Execution;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aimrank.Web.App.GraphQL.Subscriptions
{
    public class EmptySourceStream<TMessage> : ISourceStream<TMessage>
    {
        public IAsyncEnumerable<TMessage> ReadEventsAsync()
        {
            return AsyncEnumerable.Empty<TMessage>();
        }
        
        IAsyncEnumerable<object> ISourceStream.ReadEventsAsync()
        {
            return AsyncEnumerable.Empty<object>();
        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }
    }
}