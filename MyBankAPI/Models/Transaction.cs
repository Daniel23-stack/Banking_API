namespace MyBankAPI.Models;

public class Transaction
{
    public int Id { get; set; }
    public string TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }

    // Foreign key to link to BankAccount
    public int BankAccountId { get; set; }
    public BankAccount BankAccount { get; set; }
}