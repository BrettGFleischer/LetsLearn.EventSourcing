using LetsLearn.EventSourcing.BasicEventSourcingExample.Interfaces;

namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Events;

public record BaseEvent(Guid AccountId, uint Version) : IAccountEvent
{
    public uint Id { get; internal set; }
    public DateTime EventDate { get; internal set; }
}