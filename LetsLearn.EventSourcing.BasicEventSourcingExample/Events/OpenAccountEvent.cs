namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Events;

public record OpenAccountEvent(Guid AccountId, uint Version) : BaseEvent(AccountId, Version);