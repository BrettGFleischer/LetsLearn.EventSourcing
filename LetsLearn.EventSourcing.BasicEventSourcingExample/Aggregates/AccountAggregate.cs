using LetsLearn.EventSourcing.BasicEventSourcingExample.Commands;
using LetsLearn.EventSourcing.BasicEventSourcingExample.Events;
using LetsLearn.EventSourcing.BasicEventSourcingExample.Interfaces;
using LetsLearn.EventSourcing.BasicEventSourcingExample.Persistence;
using LetsLearn.EventSourcing.BasicEventSourcingExample.ViewModels;

namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Aggregates;

public class AccountAggregate
{
    private Guid AccountId { get; set; }
    private decimal Balance { get; set; }
    private bool Active { get; set; }
    private uint Version { get; set; }
    private DateTime LastModifiedDate { get; set; }
    private DateTime CreatedDate { get; set; }

    private readonly InMemoryDbContext _dbContext;

    public AccountAggregate(InMemoryDbContext dbContext)
    {
        _dbContext = dbContext;
        Active = false;
        Version = 0;
    }

    public AccountAggregate(InMemoryDbContext dbContext, Guid accountId)
    {
        _dbContext = dbContext;

        var baseEvents = _dbContext.BaseEvents.Where(e => e.AccountId == accountId).OrderBy(a => a.Version).ToList();

        if (baseEvents.Count < 0)
        {
            throw new Exception($"Account with Id '{accountId}' does not exist");
        }

        ApplyEvents(baseEvents);
    }

    public void HandleOpenAccountCommand(OpenAccountCommand openAccountCommand)
    {
        if (Version != 0)
        {
            throw new ArgumentException("Account has already been opened.");
        }

        var openAccountEvent = new OpenAccountEvent(openAccountCommand.AccountId, NextVersion());

        PersistAndApplyEvent(openAccountEvent);
    }

    public void HandleDepositCommand(DepositCommand depositCommand)
    {
        if (!Active)
        {
            throw new ArgumentException("Account is not active.");
        }

        var depositAmount = depositCommand.Amount;

        if (depositAmount <= 0)
        {
            throw new ArgumentException("Amount must be positive.");
        }

        var depositEvent = new DepositEvent(AccountId, NextVersion())
        {
            Amount = depositAmount
        };

        PersistAndApplyEvent(depositEvent);
    }

    public void HandleWithdrawalCommand(WithdrawalCommand withdrawalCommand)
    {
        if (!Active)
        {
            throw new ArgumentException("Account is not active.");
        }

        var withdrawalAmount = withdrawalCommand.Amount;

        if (withdrawalAmount <= 0)
        {
            throw new ArgumentException("Amount must be positive.");
        }

        var newBalance = Balance - withdrawalAmount;

        if (newBalance < 0)
        {
            throw new Exception("Not enough funds for Withdrawal");
        }

        var withdrawalEvent = new WithdrawalEvent(AccountId, NextVersion())
        {
            Amount = withdrawalAmount
        };
        
        PersistAndApplyEvent(withdrawalEvent);
    }

    public void HandleActivateAccountCommand(ActivateAccountCommand _)
    {
        if (Active)
        {
            throw new ArgumentException("Account is already activated.");
        }

        var activateAccountEvent = new ActivateAccountEvent(AccountId, NextVersion());

        PersistAndApplyEvent(activateAccountEvent);
    }

    public void HandleDeactivateAccountCommand(DeactivateAccountCommand _)
    {
        if (!Active)
        {
            throw new ArgumentException("Account is already deactivated.");
        }

        var deactivateAccountEvent = new DeactivateAccountEvent(AccountId, NextVersion());
        
        PersistAndApplyEvent(deactivateAccountEvent);
    }

    public AccountViewModel GetAccountView()
    {
        return new AccountViewModel
        {
            AccountId = AccountId,
            Balance = Balance,
            Version = Version,
            LastModifiedDate = LastModifiedDate,
            CreatedDate = CreatedDate
        };
    }

    private void Apply(OpenAccountEvent openAccountEvent)
    {
        AccountId = openAccountEvent.AccountId;
        CreatedDate = openAccountEvent.EventDate;
        UpdateAuditProperties(openAccountEvent);
    }

    private void Apply(DepositEvent depositEvent)
    {
        Balance += depositEvent.Amount;
        UpdateAuditProperties(depositEvent);
    }

    private void Apply(WithdrawalEvent withdrawalEvent)
    {
        Balance -= withdrawalEvent.Amount;
        UpdateAuditProperties(withdrawalEvent);
    }

    private void Apply(ActivateAccountEvent activateAccountEvent)
    {
        Active = true;
        UpdateAuditProperties(activateAccountEvent);
    }

    private void Apply(DeactivateAccountEvent deactivateAccountEvent)
    {
        Active = false;
        UpdateAuditProperties(deactivateAccountEvent);
    }

    private void ApplyEvents(IEnumerable<BaseEvent> baseEvents)
    {
        foreach (var baseEvent in baseEvents)
        {
            ApplyEvent(baseEvent);
        }
    }

    private void ApplyEvent(BaseEvent baseEvent)
    {
        switch (baseEvent)
        {
            case OpenAccountEvent @event:
                Apply(@event);
                break;
            case DepositEvent @event:
                Apply(@event);
                break;
            case WithdrawalEvent @event:
                Apply(@event);
                break;
            case ActivateAccountEvent @event:
                Apply(@event);
                break;
            case DeactivateAccountEvent @event:
                Apply(@event);
                break;
        }
    }

    private void PersistEvent(BaseEvent baseEvent)
    {
        _dbContext.Add(baseEvent);
        _dbContext.SaveChanges();
    }

    private void PersistAndApplyEvent(BaseEvent baseEvent)
    {
        PersistEvent(baseEvent);
        ApplyEvent(baseEvent);
    }

    private void UpdateAuditProperties(IAccountEvent accountEvent)
    {
        LastModifiedDate = accountEvent.EventDate;
        Version = accountEvent.Version;
    }

    private uint NextVersion()
    {
        return Version + 1;
    }
}