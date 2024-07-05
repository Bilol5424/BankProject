using System.Text.RegularExpressions;

namespace BankProject.Services;

public class CommandService
{
    private AccountService AccountService { get; set; } = new();

    public void AddAccount()
    {
        string accountNumber;
        while (true)
        {
            Console.WriteLine("Введите номер аккаунта:");
            var readLine = Console.ReadLine();

            if (readLine is null)
                Console.WriteLine($"Некорректный ввод: {readLine}");

            if (Regex.IsMatch(readLine!, "\\d{20}"))
            {
                accountNumber = readLine!;
                break;
            }

            Console.WriteLine($"Некорректный ввод: {readLine}");
        }

        string owner;
        while (true)
        {
            Console.Write("Введите ФИО владельца:");
            var readLine = Console.ReadLine();

            if (readLine is null)
            {
                Console.WriteLine($"Некорректный ввод: {readLine}");
            }

            var pattern = "^[А-ЯЁA-Z][а-яёa-z]+ [А-ЯЁA-Z][а-яёa-z]+$";

            if (Regex.IsMatch(readLine!, pattern))
            {
                owner = readLine!;
                break;
            }

            Console.WriteLine($"Некорректный ввод: {readLine}");
        }

        decimal balance;
        while (true)
        {
            Console.Write("Введите начальный баланс: ");
            var readLine = Console.ReadLine();

            if (decimal.TryParse(readLine, out var tmp))
            {
                if (tmp < 0)
                {
                    Console.WriteLine("Началный баланс не может быть отрецательным");
                }
                else
                {
                    balance = tmp;
                    break;
                }
            }
            else
            {
                Console.WriteLine("Началный баланс не может быть пустым");
            }
        }

        AccountService.Create(accountNumber, owner, balance);
    }

    public void Deposit()
    {
        Console.WriteLine("Введите номер счета: ");
        var accountNumber = Console.ReadLine();
        Console.WriteLine("Введите сумму для пополнения: ");

        decimal amount;

        while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        {
            Console.WriteLine("Некорректная сумма. Введите положительное число.");
        }

        AccountService.Deposit(accountNumber!, amount);
    }

    public void Withdraw()
    {
        Console.WriteLine("Введите номер счета: ");
        var accountNumber = Console.ReadLine();

        Console.WriteLine("Введите сумму для списания: ");
        decimal amount;

        while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        {
            Console.WriteLine("Некорректная сумма. Введите положительное число.");
        }

        AccountService.Withdraw(accountNumber!, amount);
    }

    public void ShowAccounts()
    {
        var accounts = AccountService.GetAllAccounts();
        Console.WriteLine("Список всех аккаунтов:");

        foreach (var account in accounts)
        {
            Console.WriteLine(
                $"Номер аккаунта:{account.Number}, ФИО владелца: {account.Owner}, баланс: {account.Balance}.");
        }
    }

    public void SendMoney()
    {
        string fromAccountNumber;
        while (true)
        {
            Console.WriteLine("Введите счёт отправителя:");
            var readLine = Console.ReadLine();

            if (readLine is null)
                Console.WriteLine($"Некорректный ввод: {readLine}");

            if (Regex.IsMatch(readLine!, "\\d{20}"))
            {
                fromAccountNumber = readLine!;
                break;
            }

            Console.WriteLine($"Некорректный ввод: {readLine}");
        }

        string toAccountNumber;
        while (true)
        {
            Console.WriteLine("Введите счёта получателя:");
            var readLine = Console.ReadLine();

            if (readLine is null)
                Console.WriteLine($"Некорректный ввод: {readLine}");

            if (Regex.IsMatch(readLine!, "\\d{20}"))
            {
                toAccountNumber = readLine!;
                break;
            }

            Console.WriteLine($"Некорректный ввод: {readLine}");
        }

        Console.WriteLine("Введите сумму начисления:");
        decimal amount;

        while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        {
            Console.WriteLine("Некорректная сумма.");
        }

        AccountService.SendMoney(fromAccountNumber, toAccountNumber, amount);
    }
}