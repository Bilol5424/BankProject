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
            commandService.Withdraw();
            break;
        
        case 3:
            commandService.ShowAccounts();
            
            break;
        
        case 4:
            commandService.Deposit();
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