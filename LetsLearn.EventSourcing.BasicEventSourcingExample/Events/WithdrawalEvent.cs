namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Events;

public record WithdrawalEvent(Guid AccountId, uint Version) : BaseEvent(AccountId, Version)
{
    public required decimal Amount { get; set; }
}