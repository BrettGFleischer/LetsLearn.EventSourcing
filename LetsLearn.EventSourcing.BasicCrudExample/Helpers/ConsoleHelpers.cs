using LetsLearn.EventSourcing.BasicCrudExample.Models;

namespace LetsLearn.EventSourcing.BasicCrudExample.Helpers;

public static class ConsoleHelpers
{
    public static void LogAccountInfo(IEnumerable<Account?> accounts)
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
                account.Id,
                account.Balance,
                account.CreatedDate,
                account.LastModifiedDate);
        }

        Console.WriteLine(new string('=', 100));
    }
}