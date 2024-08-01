namespace LetsLearn.EventSourcing.BasicCrudExample.Interfaces;

public interface IAuditable
{
    public DateTime LastModifiedDate { get; set; }
    public DateTime CreatedDate { get; set; }
}