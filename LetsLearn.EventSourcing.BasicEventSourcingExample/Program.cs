using LetsLearn.EventSourcing.BasicEventSourcingExample.Contexts;
using LetsLearn.EventSourcing.BasicEventSourcingExample.Helpers;

var accountContext = new AccountContext();

// Open a new account
var account = accountContext.OpenAccount();
var newAccountId = account.AccountId;

// Simulate some delay
Thread.Sleep(1000);

// Deposit money
accountContext.Deposit(newAccountId, 2000);
// Withdraw money...
accountContext.Withdraw(newAccountId, 500);

// Simulate some delay
Thread.Sleep(1000);

// Withdraw more money...
accountContext.Withdraw(newAccountId, 50);

// View the current state of the account
ConsoleHelpers.LogAccountInfo([accountContext.GetAccountById(newAccountId)]);

// View all events of all accounts
ConsoleHelpers.LogEventInfo(accountContext.GetAllEvents());

Console.ReadLine();