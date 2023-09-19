using Microsoft.AspNetCore.Mvc;
using MyBankAPI.Models;
using MyBankAPI.Repository;


[Route("api/accounts")]
[ApiController]
public class BankAccountController : ControllerBase
{
    
    private readonly IBankAccountService _service;
    private readonly BankAccountRepository _repository;

    public BankAccountController(IBankAccountRepository repository, IBankAccountService service, BankAccountRepository repository1)
    {
        _service = service;
        _repository = repository1;
    }

    [HttpGet("{accountHolderId}")]
    public async Task<IActionResult> GetAccounts(int accountHolderId)
    {
        var accounts = await _service.GetAccountsByAccountHolderIdAsync(accountHolderId);
        return Ok(accounts);
    }

    [HttpGet("{accountNumber}")]
    public async Task<IActionResult> GetAccount(string accountNumber)
    {
        var account = await _service.GetAccountByAccountNumberAsync(accountNumber);
        if (account == null)
        {
            return NotFound();
        }
        return Ok(account);
    }

    [HttpPost("withdrawals")]
    public async Task<IActionResult> CreateWithdrawal(Withdrawal withdrawal)
    {
        if (withdrawal.Amount <= 0)
        {
            return BadRequest("Withdrawal amount must be greater than 0.");
        }

        var account = await _service.GetAccountByAccountNumberAsync(withdrawal.AccountNumber);
        if (account == null)
        {
            return NotFound("Account not found.");
        }

        if (account.Status == "Inactive")
        {
            return BadRequest("Withdrawals are not allowed on inactive accounts.");
        }

        if (account.AccountType == "Fixed Deposit" && withdrawal.Amount != account.AvailableBalance)
        {
            return BadRequest("Only 100% withdrawal is allowed for Fixed Deposit accounts.");
        }

        if (withdrawal.Amount > account.AvailableBalance)
        {
            return BadRequest("Withdrawal amount exceeds the available balance.");
        }
        var success = await _service.CreateWithdrawalAsync(withdrawal);
        if (success)
        {
            return Ok("Withdrawal successful.");
        }
        else
        {
            return BadRequest("Withdrawal failed. Please try again later.");
        }
    }
}
