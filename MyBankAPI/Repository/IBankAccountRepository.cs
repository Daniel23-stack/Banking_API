using MyBankAPI.Models;

namespace MyBankAPI.Repository;

public interface IBankAccountRepository
{
    Task<List<BankAccount>> GetAccountsByAccountHolderIdAsync(int accountHolderId);
    Task<BankAccount> GetAccountByAccountNumberAsync(string accountNumber);
    Task<bool> CreateWithdrawalAsync(Withdrawal withdrawal);
    Task SaveChangesAsync();
}