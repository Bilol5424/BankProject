using System.Text.RegularExpressions;
using BankProject.Entities;

namespace BankProject.Services;

public class AccountService
{
    private static List<Account> Accounts { get; set; } = new();

    public bool Create(string accountNumber, string owner, decimal balance)
    {
        var account = Accounts.FirstOrDefault(x => x.Number == accountNumber);

        if (account is not null)
        {
            Console.WriteLine("Счет уже сеществуеит!");
            return false;
        }

        if (balance < 0)
        {
            Console.WriteLine("Неправельный баланс");
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
            $"Счёт владелца {account.Owner} создан с номером: {account.Number} и  c началным балансом {account.Balance}.");
        Console.ResetColor();

        return true;
    }

    public void ShowOllAccounts()
    {
        Console.WriteLine("Список всех счётов:");
        foreach (var account in Accounts)
        {
            Console.WriteLine(account);
        }
    }

    public bool Deposit(string accountNumber, decimal amount)
    {
        var account = Accounts.FirstOrDefault(x => x.Number == accountNumber);
        account = GetAccount(accountNumber);
                
        Console.WriteLine($"Введите необходимую сумму:");
        decimal num = Convert.ToDecimal(Console.ReadLine());
        /*account.Deposit(num);*/
                
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Сумма {num} начислена на счёт {account.Number}. Текущий баланс: {account.Balance}.");
        Console.ResetColor();
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

        Console.WriteLine($"Сумма: {amount} успешно зачислена. обновлённый баланс: {account.Balance}");

        return true;
    }

    public bool Withdraw(string accountNumber, decimal amount)
    {
        var account = Accounts.FirstOrDefault(x => x.Number == accountNumber);
          
        account = GetAccount(accountNumber);

        Console.WriteLine("Введите сумму списания: ");
        decimal mun = Convert.ToDecimal(Console.ReadLine());
     

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Сумма {mun} списана со счёта {account.Number}. Текущий баланс: {account.Balance}.");
        Console.ResetColor();
    
            if (account.Balance < amount)
            {
                Console.WriteLine($"На вашем счету недостаточно средств для списания необходимой суммы. Текущий баланс: {account.Balance} нехватающая сумма: {amount - account.Balance}.");
                return false;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Сумма должна быть положительной");
                return false;
            }
            account.Balance -= amount;
            Console.WriteLine($"Снято {amount:C}. Новый баланс: {account.Balance:C}.");
            return true;
            
        
    }
    public Account GetAccount(string accountNumber)
    {
        return Accounts.Find(a => a.Number == accountNumber);
    }

  
}