using LetsLearn.EventSourcing.BasicCrudExample.Interfaces;

namespace LetsLearn.EventSourcing.BasicCrudExample.Models;

public record Account : IAuditable
{
    public Guid Id { get; set; }
    public required decimal Balance { get; set; }
    public DateTime LastModifiedDate { get; set; }
    public DateTime CreatedDate { get; set; }
}