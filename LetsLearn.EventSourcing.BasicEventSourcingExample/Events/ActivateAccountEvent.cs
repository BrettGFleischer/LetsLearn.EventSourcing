namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Events;

public record ActivateAccountEvent(Guid AccountId, uint Version) : BaseEvent(AccountId, Version);