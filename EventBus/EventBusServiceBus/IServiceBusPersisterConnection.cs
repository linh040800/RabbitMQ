using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusServiceBus
{
    public interface IServiceBusPersisterConnection : IDisposable
    {
        ITopicClient TopicClient { get; }
        ISubscriptionClient SubscriptionClient { get; }
    }
}
