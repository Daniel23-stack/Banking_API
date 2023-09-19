namespace MyBankAPI.Models;

public class AccountAuditTrail
{
    public int Id { get; set; }
    public string Action { get; set; }
    public DateTime Timestamp { get; set; }

    // Foreign key to link to BankAccount
    public int BankAccountId { get; set; }
    public BankAccount BankAccount { get; set; }
}