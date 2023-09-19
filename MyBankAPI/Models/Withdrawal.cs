namespace MyBankAPI.Models;

public class Withdrawal
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }

    // Foreign key to link to BankAccount
    public int BankAccountId { get; set; }
    public BankAccount BankAccount { get; set; }
}