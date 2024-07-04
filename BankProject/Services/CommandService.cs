using System.Text.RegularExpressions;
namespace BankProject.Services;

public class CommandService
{
    public AccountService AccountService { get; set; } = new();

    public bool AddAccount()
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

            var pattern = "^[А-ЯЁA-Z][а-яёa-z]+[А-ЯЁA-Z][а-яёa-z]+([А-ЯЁA-Z][а-яёa-z])?$";

            if (Regex.IsMatch(readLine!, pattern))
            {
                owner = readLine!;
                break;
            }

            Console.WriteLine($"Некорректный ввод: {readLine}");
        }

        decimal balance = default;
        while (true)
        {
            Console.Write("Введите начальный баланс: ");
            var readLine = Console.ReadLine();
            
            if (decimal.TryParse(readLine, out var tmp))
            {
                balance = tmp;
                break;
            }

            if (balance < 0)
            {
                Console.WriteLine("Началный баланс не может быть отрецательным");
            }
            else
            {
                Console.WriteLine("Началный баланс не может быть пустым");
                break;
            }
        }

        AccountService.Create(accountNumber, owner, balance);

        return true;
    }
}