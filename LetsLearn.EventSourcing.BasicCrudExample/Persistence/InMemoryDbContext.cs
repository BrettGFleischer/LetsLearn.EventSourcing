using LetsLearn.EventSourcing.BasicCrudExample.Interfaces;
using LetsLearn.EventSourcing.BasicCrudExample.Models;
using Microsoft.EntityFrameworkCore;

namespace LetsLearn.EventSourcing.BasicCrudExample.Persistence;

public class InMemoryDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("LetsLearn.EventSourcing.BasicCrudExample.InMemoryDatabase");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(
            account =>
            {
                account.HasIndex(a => a.Id)
                    .IsUnique();
                account.Property(a => a.Id)
                    .ValueGeneratedOnAdd();
            }
        );
    }

    public override int SaveChanges()
    {
        var currentDateTime = DateTime.UtcNow;
        var entries = ChangeTracker.Entries().ToList();

        var updatedEntries = entries.Where(e => e.Entity is IAuditable)
            .ToList();

        updatedEntries.ForEach(e =>
        {
            switch (e.State)
            {
                case EntityState.Modified:
                    ((IAuditable)e.Entity).LastModifiedDate = currentDateTime;
                    break;
                case EntityState.Added:
                    ((IAuditable)e.Entity).CreatedDate = currentDateTime;
                    ((IAuditable)e.Entity).LastModifiedDate = currentDateTime;
                    break;
            }
        });

        return base.SaveChanges();
    }
}