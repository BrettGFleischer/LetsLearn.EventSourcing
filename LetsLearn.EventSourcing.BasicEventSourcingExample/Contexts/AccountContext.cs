using LetsLearn.EventSourcing.BasicEventSourcingExample.Aggregates;
using LetsLearn.EventSourcing.BasicEventSourcingExample.Commands;
using LetsLearn.EventSourcing.BasicEventSourcingExample.Events;
using LetsLearn.EventSourcing.BasicEventSourcingExample.Persistence;
using LetsLearn.EventSourcing.BasicEventSourcingExample.ViewModels;

namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Contexts;

public class AccountContext : IDisposable
{
    private readonly InMemoryDbContext _dbContext;

    public AccountContext()
    {
        _dbContext = new InMemoryDbContext();
        _dbContext.Database.EnsureCreated();
    }

    // Commands
    public AccountViewModel OpenAccount()
    {
        var accountAggregate = new AccountAggregate(_dbContext);
        var newAccountId = Guid.NewGuid();

        accountAggregate.HandleOpenAccountCommand(new OpenAccountCommand(newAccountId));
        accountAggregate.HandleActivateAccountCommand(new ActivateAccountCommand());

        return accountAggregate.GetAccountView();
    }

    public AccountViewModel ActivateAccount(Guid accountId)
    {
        var accountAggregate = new AccountAggregate(_dbContext, accountId);

        accountAggregate.HandleActivateAccountCommand(new ActivateAccountCommand());

        return accountAggregate.GetAccountView();
    }

    public AccountViewModel DeactivateAccount(Guid accountId)
    {
        var accountAggregate = new AccountAggregate(_dbContext, accountId);

        accountAggregate.HandleDeactivateAccountCommand(new DeactivateAccountCommand());

        return accountAggregate.GetAccountView();
    }

    public AccountViewModel Deposit(Guid accountId, decimal amount, Guid transactionId)
    {
        var accountAggregate = new AccountAggregate(_dbContext, accountId);

        accountAggregate.HandleDepositCommand(new DepositCommand(amount, transactionId));

        return accountAggregate.GetAccountView();
    }

    public AccountViewModel Withdraw(Guid accountId, decimal amount, Guid transactionId)
    {
        var accountAggregate = new AccountAggregate(_dbContext, accountId);

        accountAggregate.HandleWithdrawalCommand(new WithdrawalCommand(amount, transactionId));

        return accountAggregate.GetAccountView();
    }

    // Queries
    public AccountViewModel GetAccountById(Guid accountId)
    {
        return new AccountAggregate(_dbContext, accountId).GetAccountView();
    }

    public List<BaseEvent> GetAllEvents()
    {
        return _dbContext.BaseEvents.ToList();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}