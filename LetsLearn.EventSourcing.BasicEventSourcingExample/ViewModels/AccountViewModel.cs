namespace LetsLearn.EventSourcing.BasicEventSourcingExample.ViewModels;

public record AccountViewModel
{
    public required Guid AccountId { get; init; }
    public required decimal Balance { get; init; }
    public required uint Version { get; init; }
    public required DateTime LastModifiedDate { get; init; }
    public required DateTime CreatedDate { get; init; }
}