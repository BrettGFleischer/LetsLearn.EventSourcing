namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Events;

public record DepositEvent(Guid AccountId, uint Version) : BaseEvent(AccountId, Version)
{
    public required decimal Amount { get; set; }
    public required Guid TransactionId { get; set; }
}

public record DepositEventV2(Guid AccountId, uint Version, decimal Amount, Guid TransactionId)
    : BaseEvent(AccountId, Version);