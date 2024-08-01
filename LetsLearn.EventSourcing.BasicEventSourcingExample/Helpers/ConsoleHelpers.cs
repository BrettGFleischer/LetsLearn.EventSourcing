using LetsLearn.EventSourcing.BasicEventSourcingExample.Events;
using LetsLearn.EventSourcing.BasicEventSourcingExample.ViewModels;

namespace LetsLearn.EventSourcing.BasicEventSourcingExample.Helpers;

public static class ConsoleHelpers
{
    public static void LogAccountInfo(IEnumerable<AccountViewModel?> accounts)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(new string('=', 100));
        Console.WriteLine("{0,-40} {1,-10} {2,-20} {3,-20}", "Account Id", "Balance", "Created Date",
            "Last Modified Date");
        Console.WriteLine(new string('-', 100));

        foreach (var account in accounts)
        {
            if (account is null)
            {
                continue;
            }

            Console.WriteLine("{0,-40} {1,-10} {2,-20} {3,-20}",
                account.AccountId,
                account.Balance,
                account.CreatedDate,
                account.LastModifiedDate);
        }

        Console.WriteLine(new string('=', 100));
    }

    public static void LogEventInfo(IEnumerable<BaseEvent> baseEvents)
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(new string('=', 120));
        Console.WriteLine("{0,-40} {1,-10} {2,-20} {3,-20} {4,-20}",
            "Account Id",
            "Event Id",
            "Event Type",
            "Balance Diff",
            "Event Date");
        Console.WriteLine(new string('-', 120));

        foreach (var baseEvent in baseEvents.OrderBy(e => e.AccountId).ThenBy(e => e.Version))
        {
            switch (baseEvent)
            {
                case OpenAccountEvent @event:
                    WriteEventToConsole(@event.AccountId, @event.Id, "OpenAccountEvent", "0", @event.EventDate);
                    break;
                case DepositEvent @event:
                    WriteEventToConsole(@event.AccountId, @event.Id, "DepositEvent", $"+{@event.Amount}",
                        @event.EventDate);
                    break;
                case WithdrawalEvent @event:
                    WriteEventToConsole(@event.AccountId, @event.Id, "WithdrawalEvent", $"-{@event.Amount}",
                        @event.EventDate);
                    break;
                case ActivateAccountEvent @event:
                    WriteEventToConsole(@event.AccountId, @event.Id, "ActivateAccountEvent", "0", @event.EventDate);
                    break;
                case DeactivateAccountEvent @event:
                    WriteEventToConsole(@event.AccountId, @event.Id, "DeactivateAccountEvent", "0", @event.EventDate);
                    break;
            }
        }

        Console.WriteLine(new string('=', 120));

        return;

        // local functions

        static void WriteEventToConsole(Guid accountId,
            uint eventId,
            string eventType,
            string balanceDiff,
            DateTime lastModified)
        {
            Console.WriteLine("{0,-40} {1,-10} {2,-20} {3,-20} {4,-20}",
                $"{accountId}",
                $"{eventId}",
                eventType,
                balanceDiff,
                $"{lastModified}");
        }
    }
}