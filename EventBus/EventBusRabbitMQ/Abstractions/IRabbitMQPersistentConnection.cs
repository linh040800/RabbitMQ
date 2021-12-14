using RabbitMQ.Client;
using System;

namespace PO.EventBusRabbitMQ.Abstractions
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}
