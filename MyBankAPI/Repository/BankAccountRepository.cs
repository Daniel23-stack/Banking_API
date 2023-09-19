using Microsoft.EntityFrameworkCore;
using MyBankAPI.Data;
using MyBankAPI.Models;

namespace MyBankAPI.Repository;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BankAccountRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<BankAccount>> GetAccountsByAccountHolderIdAsync(int accountHolderId)
    {
        return await _dbContext.BankAccounts
            .Where(account => account.AccountHolderId == accountHolderId)
            .ToListAsync();
    }
    public async Task<BankAccount> GetAccountByAccountNumberAsync(string accountNumber)
    {
        return await _dbContext.BankAccounts
            .SingleOrDefaultAsync(account => account.AccountNumber == accountNumber);
    }
    public async Task<bool> CreateWithdrawalAsync(Withdrawal withdrawal)
    {
        // Implement withdrawal logic here
        // Make sure to apply the specified validation criteria
        // Example: Check withdrawal amount, account status, etc.

        // If withdrawal is successful, update the account balance and status
        // and create an audit trail entry

        return true;
    }

    public async Task SaveChangesAsync()
    {
        
    }
}