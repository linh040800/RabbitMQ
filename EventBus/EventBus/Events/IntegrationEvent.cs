using System;
using System.Text.Json.Serialization;

namespace PO.EventBus.Events
{
    /// <summary>
    /// Event model 
    /// </summary>
    public record IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonInclude]
        public Guid Id { get; private init; }

        [JsonInclude]
        public string Method { get; private init; }

        [JsonInclude]
        public DateTime CreationDate { get; private init; }

    }
}
