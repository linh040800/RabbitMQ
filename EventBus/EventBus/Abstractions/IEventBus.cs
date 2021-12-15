using PO.EventBus.Events;

namespace PO.EventBus.Abstractions
{
    /// <summary>
    ///List of basic Eventbus methods
    /// </summary>
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);

        void InitPublish<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
    }
}
