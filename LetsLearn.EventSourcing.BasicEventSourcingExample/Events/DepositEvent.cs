namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Events;

public record DepositEvent(Guid AccountId, uint Version) : BaseEvent(AccountId, Version)
{
    public required decimal Amount { get; set; }
}