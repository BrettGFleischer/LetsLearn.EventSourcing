using LetsLearn.EventSourcing.BasicEventSourcingExample.Interfaces;

namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Events;

public record BaseEvent(Guid AccountId, uint Version) : IAccountEvent
{
    public uint Id { get; set; }
    public DateTime EventDate { get; set; }
}