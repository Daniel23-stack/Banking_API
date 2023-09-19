
using MyBankAPI.Models;

public interface IBankAccountService
{
    Task<List<BankAccount>> GetAccountsByAccountHolderIdAsync(int accountHolderId);
    Task<BankAccount> GetAccountByAccountNumberAsync(string accountNumber);
    Task<bool> CreateWithdrawalAsync(Withdrawal withdrawal);
}