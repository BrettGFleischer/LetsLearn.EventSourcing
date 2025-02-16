namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Interfaces;

public interface IAccountEvent
{
    public uint Id { get; }
    public Guid AccountId { get; init; }
    public uint Version { get; }
    public DateTime EventDate { get; }
}