using LetsLearn.EventSourcing.BasicCrudExample.Contexts;
using LetsLearn.EventSourcing.BasicCrudExample.Helpers;

var accountContext = new AccountContext();

// Open a new account
var account = accountContext.OpenAccount();
var newAccountId = account.Id;

// Simulate some delay
Thread.Sleep(1000);

// Deposit money
var firstDepositResult = accountContext.Deposit(newAccountId, 2000);
// Withdraw money...
var firstWithdrawalResult = accountContext.Withdraw(newAccountId, 500);

// Simulate some delay
Thread.Sleep(1000);

// Withdraw more money...
var secondWithdrawalResult = accountContext.Withdraw(newAccountId, 50);

// View the current state of the account
ConsoleHelpers.LogAccountInfo([accountContext.GetAccountById(newAccountId)]);

Console.ReadLine();