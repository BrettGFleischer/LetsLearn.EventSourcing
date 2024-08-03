using LetsLearn.EventSourcing.BasicCrudExample.Models;
using LetsLearn.EventSourcing.BasicCrudExample.Persistence;

namespace LetsLearn.EventSourcing.BasicCrudExample.Contexts;

public class AccountContext : IDisposable
{
    private readonly InMemoryDbContext _dbContext;

    public AccountContext()
    {
        _dbContext = new InMemoryDbContext();
        _dbContext.Database.EnsureCreated();
    }

    public Account OpenAccount()
    {
        var account = new Account
        {
            Balance = 0
        };

        _dbContext.Add(account);
        _dbContext.SaveChanges();

        return account;
    }

    public Account Deposit(Guid accountId, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive.");
        }

        var account = GetAccountById(accountId) ??
                      throw new Exception($"Account with Id '{accountId}' does not exist");

        account.Balance += amount;
        _dbContext.SaveChanges();

        return account;
    }

    public Account Withdraw(Guid accountId, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive.");
        }

        var account = GetAccountById(accountId) ??
                      throw new Exception($"Account with Id '{accountId}' does not exist");

        var newBalance = account.Balance - amount;

        if (newBalance < 0)
        {
            throw new Exception("Not enough funds for Withdrawal");
        }

        account.Balance = newBalance;
        _dbContext.SaveChanges();

        return account;
    }

    public Account CloseAccount(Guid id)
    {
        var account = GetAccountById(id);

        if (account is null)
        {
            throw new Exception($"Account with Id '{id}' does not exist");
        }

        var result = _dbContext.Remove(account);
        _dbContext.SaveChanges();

        return result.Entity;
    }

    public Account? GetAccountById(Guid id)
    {
        return _dbContext.Accounts.SingleOrDefault(a => a.Id == id);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}