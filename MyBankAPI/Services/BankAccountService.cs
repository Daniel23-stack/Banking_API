using MyBankAPI.Data;
using MyBankAPI.Models;
using MyBankAPI.Repository;

namespace MyBankAPI.Services;

public class BankAccountService:IBankAccountService
{
    private readonly IBankAccountRepository _repository;
    private readonly ApplicationDbContext _context;

    public BankAccountService(IBankAccountRepository repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<List<BankAccount>> GetAccountsByAccountHolderIdAsync(int accountHolderId)
    {
        return await _repository.GetAccountsByAccountHolderIdAsync(accountHolderId);
    }

    public async Task<BankAccount> GetAccountByAccountNumberAsync(string accountNumber)
    {
        return await _repository.GetAccountByAccountNumberAsync(accountNumber);
    }
    
    public async Task<bool> CreateWithdrawalAsync(Withdrawal withdrawal)
    {
        // Retrieve the account by account number
        var account = await _repository.GetAccountByAccountNumberAsync(withdrawal.AccountNumber);

        if (account == null)
        {
            return false; // Account not found
        }

        if (account.Status == "Inactive")
        {
            return false; // Withdrawals not allowed on inactive accounts
        }
        
        decimal newBalance = account.AvailableBalance - withdrawal.Amount;

        // Apply withdrawal rules based on account type
        if (account.AccountType == "Fixed Deposit")
        {
            if (withdrawal.Amount != account.AvailableBalance)
            {
                return false; // Only 100% withdrawal is allowed for Fixed Deposit accounts
            }
        }
        else
        {
            if (withdrawal.Amount <= 0 || withdrawal.Amount > account.AvailableBalance)
            {
                return false; // Invalid withdrawal amount
            }
        }
        // Create a withdrawal transaction
        var withdrawalTransaction = new Transaction
        {
            Id = account.Id,
            TransactionType = "Withdrawal",
            Amount = withdrawal.Amount,
            TransactionDate = DateTime.UtcNow
        };
        var auditTrail = new AccountAuditTrail
        {
            Id = account.Id,
            Action = "Withdrawal",
            Timestamp = DateTime.UtcNow
        };

        // Update the account's available balance and last modified timestamp
        account.AvailableBalance = newBalance;
        account.LastModified = DateTime.UtcNow;
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Save the withdrawal transaction
                _context.Transactions.Add(withdrawalTransaction);

                // Save the audit trail entry
                _context.AccountAuditTrails.Add(auditTrail);

                // Save the updated account data
                _context.BankAccounts.Update(account);

                // Commit the transaction
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true; // Withdrawal successful
            }
            catch (Exception)
            {
                // Handle database errors or concurrency issues
                await transaction.RollbackAsync();
                return false; // Withdrawal failed
            }
        }
    }
}