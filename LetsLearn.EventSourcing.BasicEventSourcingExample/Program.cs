using LetsLearn.EventSourcing.BasicEventSourcingExample.Contexts;
using LetsLearn.EventSourcing.BasicEventSourcingExample.Helpers;

var accountContext = new AccountContext();

// Open a new account
var account = accountContext.OpenAccount();
var newAccountId = account.AccountId;

// Simulate some delay
Thread.Sleep(1000);

// Deposit money
var firstDepositId = Guid.NewGuid();
var firstDepositResult = accountContext.Deposit(newAccountId, 2000, firstDepositId);
// Withdraw money...
var firstWithdrawalId = Guid.NewGuid();
var firstWithdrawalResult = accountContext.Withdraw(newAccountId, 500, firstWithdrawalId);

// Simulate some delay
Thread.Sleep(1000);

// Withdraw more money...
var secondWithdrawalId = Guid.NewGuid();
var secondWithdrawalResult = accountContext.Withdraw(newAccountId, 50, secondWithdrawalId);

// Uncomment to run the last transaction again...
// And get Transaction 'transactionId' has already been processed for Account 'accountId'.
//var duplicateSecondWithdrawalResult = accountContext.Withdraw(newAccountId, 50, secondWithdrawalId);

// View the current state of the account
ConsoleHelpers.LogAccountInfo([accountContext.GetAccountById(newAccountId)]);

// View all events of all accounts
ConsoleHelpers.LogEventInfo(accountContext.GetAllEvents());

Console.ReadLine();