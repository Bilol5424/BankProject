namespace BankProject.Entities;

public class Account
{
    public required string Number { get; set; }
    public required string Owner { get; set; }
    public required decimal Balance { get; set; }
}