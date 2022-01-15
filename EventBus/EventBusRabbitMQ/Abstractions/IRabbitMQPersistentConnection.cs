using RabbitMQ.Client;
using System;

namespace Hub.EventBusRabbitMQ.Abstractions
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
