namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Events;

public record WithdrawalEvent(Guid AccountId, uint Version) : BaseEvent(AccountId, Version)
{
    public required decimal Amount { get; set; }
}

public record WithdrawalEventV2(Guid AccountId, uint Version, decimal Amount, Guid TransactionId)
    : BaseEvent(AccountId, Version);