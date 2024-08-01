using LetsLearn.EventSourcing.BasicEventSourcingExample.Events;
using Microsoft.EntityFrameworkCore;

namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Persistence;

public class InMemoryDbContext : DbContext
{
    public DbSet<BaseEvent> BaseEvents { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("LetsLearn.EventSourcing.BasicEventSourcingExample.InMemoryDatabase");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaseEvent>(
            baseEvent =>
            {
                baseEvent
                    .HasDiscriminator<string>("EventType")
                    .HasValue<DepositEvent>("DepositEvent")
                    .HasValue<OpenAccountEvent>("OpenAccountEvent")
                    .HasValue<WithdrawalEvent>("WithdrawalEvent")
                    .HasValue<ActivateAccountEvent>("ActivateAccountEvent")
                    .HasValue<DeactivateAccountEvent>("DeactivateAccountEvent");
                
                baseEvent.HasIndex(a => a.Id)
                    .IsUnique();
            }
        );
    }

    public override int SaveChanges()
    {
        var currentDateTime = DateTime.UtcNow;
        var entries = ChangeTracker.Entries().ToList();

        var updatedEntries = entries.Where(e => e.Entity is BaseEvent)
            .ToList();

        updatedEntries.ForEach(e =>
        {
            switch (e.State)
            {
                case EntityState.Modified or EntityState.Added:
                    ((BaseEvent)e.Entity).EventDate = currentDateTime;
                    break;
            }
        });

        return base.SaveChanges();
    }
}