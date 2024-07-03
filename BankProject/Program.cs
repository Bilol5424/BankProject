using System.Text.RegularExpressions;
using BankProject.Services;

var commandService = new CommandService();
        
while (true)
{
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine($"1.Добавить Счёт \t 2.Списание денег \t 3.Показать список всех счётов");
    Console.WriteLine($"4.Пополнить баланс \t 5.Выход из программы");
    Console.ResetColor();
    
    int command;
    while (true)
    {
        Console.WriteLine("Введите число операции: ");
        
        if(int.TryParse(Console.ReadLine(), out command))
        {
            break;
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Введите целое число.");
        }
    }
    
    switch (command)
        {
            case 1:
                commandService.AddAccount();
                break;
            
            case 2:
                Console.WriteLine("Введите номер счета: ");
                string withdrawAccountNumber = Console.ReadLine();
                Console.WriteLine("Введите сумму для списания: ");
                decimal amount;
                while (!decimal.TryParse(Console.ReadLine(), out amount) || amount <= 0)
                {
                    Console.WriteLine("Некорректная сумма. Введите положительное число.");
                }
                commandService.AccountService.Withdraw(withdrawAccountNumber, amount);
                break;
            case 3:
                CommandService.AccountService.ShowOllAccounts();
                break;
            case 4:
                Console.WriteLine("Введите номер счета: ");
                string depositAccountNumber = Console.ReadLine();
                Console.WriteLine("Введите сумму для пополнения: ");
                decimal depositAmount;
                while (!decimal.TryParse(Console.ReadLine(), out depositAmount) || depositAmount <= 0)
                {
                    Console.WriteLine("Некорректная сумма. Введите положительное число.");
                }
                commandService.AccountService.Deposit(depositAccountNumber, depositAmount);

                break;
            case 5:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Программа закончила свою работу");
                Console.ResetColor();
                return;
            default:
                Console.WriteLine("Команда не распознана. Выберите правильный номер команды.");
                break;
        }
}