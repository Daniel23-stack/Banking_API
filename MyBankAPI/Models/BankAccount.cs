namespace MyBankAPI.Models;

public class BankAccount
{
    public int Id { get; set; }            // Unique identifier for the bank account
    public string AccountNumber { get; set; }   // Account number
    public string AccountType { get; set; }     // Account type (e.g., Cheque, Savings, Fixed Deposit)
    public string Name { get; set; }            // Account holder's name
    public string Status { get; set; }          // Account status (e.g., Active, Inactive)
    public decimal AvailableBalance { get; set; }  // Available balance in the account

    // Foreign key to link the bank account to the account holder
    public int AccountHolderId { get; set; }
    
    // Navigation property to access the associated account holder
    public AccountHolder AccountHolder { get; set; }

    // Audit trail timestamp for when the bank account data is modified
    public DateTime LastModified { get; set; }
}