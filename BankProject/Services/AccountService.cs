using BankProject.Entities;

namespace BankProject.Services;

public class AccountService
{
    private static List<Account> Accounts { get; set; } = new();

    public bool Create(string accountNumber, string owner, decimal balance)
    {
        var account = Accounts.Find(x => x.Number == accountNumber);

        if (account is not null)
        {
            Console.WriteLine("Счет уже существует!");
            return false;
        }

        if (balance < 0)
        {
            Console.WriteLine("Неправильный баланс");
            return false;
        }

        account = new Account
        {
            Number = accountNumber,
            Owner = owner,
            Balance = balance
        };

        Accounts.Add(account);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(
            $"Счёт владельца {account.Owner} создан с номером: {account.Number} и с начальным балансом {account.Balance}.");
        Console.ResetColor();

        return true;
    }

    public bool Deposit(string accountNumber, decimal amount)
    {
        var account = Accounts.Find(x => x.Number == accountNumber);

        if (amount <= 0)
        {
            Console.WriteLine("Сумма депозита должна быть положительной.");
            return false;
        }

        if (account is null)
        {
            Console.WriteLine("Счет не найден!");
            return false;
        }

        account.Balance += amount;

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Сумма {amount:C} начислена на счёт {account.Number}. Текущий баланс: {account.Balance:C}.");
        Console.ResetColor();

        return true;
    }

    public bool Withdraw(string accountNumber, decimal amount)
    {
        var account = Accounts.Find(x => x.Number == accountNumber);

        if (account is null)
        {
            Console.WriteLine("Счет не найден!");
            return false;
        }

        if (amount <= 0)
        {
            Console.WriteLine("Сумма должна быть положительной.");
            return false;
        }

        if (account.Balance < amount)
        {
            Console.WriteLine(
                $"На вашем счету недостаточно средств для списания необходимой суммы. Текущий баланс: {account.Balance:C}, нехватающая сумма: {amount - account.Balance:C}.");
            return false;
        }

        account.Balance -= amount;

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Сумма {amount:C} списана со счёта {account.Number}. Текущий баланс: {account.Balance:C}.");
        Console.ResetColor();

        return true;
    }

    public List<Account> GetAllAccounts()
    {
        return Accounts;
    }

    public bool SendMoney(string fromAccountNumber, string accountNumberTo, decimal amount)
    {
        Console.WriteLine();
        var fromAccount = Accounts.Find(x => x.Number == fromAccountNumber);
        var toAccount = Accounts.Find(x => x.Number == accountNumberTo);

        if (fromAccount is null)
        {
            Console.WriteLine("Счет отправителя не найден!");
        }

        if (toAccount is null)
        {
            Console.WriteLine("Счет получателя не найден!");
            return false;
        }

        if (fromAccountNumber == accountNumberTo)
        {
            Console.WriteLine("Нельзя отправить средства самому себе!");
            return false;
        }

        if (fromAccount!.Balance < amount)
        {
            Console.WriteLine("Недостаточно средств на счёте.");
            return false;
        }

        fromAccount.Balance -= amount;
        toAccount.Balance += amount;

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(
            $"Сумма {amount:C} переведена со счёта {fromAccount.Number} на счёт {toAccount.Number}. Баланс отправителя: {fromAccount.Balance:C}, баланс получателя: {toAccount.Balance:C}.");
        Console.ResetColor();

        return true;
    }
}