namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Events;

public record DeactivateAccountEvent(Guid AccountId, uint Version) : BaseEvent(AccountId, Version);