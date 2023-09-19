using Microsoft.EntityFrameworkCore;
using MyBankAPI.Models;

namespace MyBankAPI.Data;

public class ApplicationDbContext:DbContext
{
    
    public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<AccountHolder> AccountHolders { get; set; }
    public virtual DbSet<Users> Users { get; set; }

    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Withdrawal> Withdrawals { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<AccountAuditTrail> AccountAuditTrails { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your entity models here

        // Seed account holders
        modelBuilder.Entity<AccountHolder>().HasData(
            new AccountHolder
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 15),
                IdNumber = "123456789",
                ResidentialAddress = "123 Main St, City",
                MobileNumber = "123-456-7890",
                EmailAddress  = "john@example.com"
            },
            new AccountHolder
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new DateTime(1985, 3, 20),
                IdNumber = "987654321",
                ResidentialAddress = "456 Elm St, Town",
                MobileNumber = "987-654-3210",
                EmailAddress  = "jane@example.com"
            }
        );

        // Seed bank accounts
        modelBuilder.Entity<BankAccount>().HasData(
            new BankAccount
            {
                Id = 1,
                AccountNumber = "1234567890",
                AccountType = "Cheque",
                Name = "John Doe Cheque Account",
                Status = "Active",
                AvailableBalance = 100000,
                AccountHolderId = 1
            },
            new BankAccount
            {
                Id = 2,
                AccountNumber = "9876543210",
                AccountType = "Savings",
                Name = "Jane Smith Savings Account",
                Status = "Active",
                AvailableBalance = 250000,
                AccountHolderId = 2
            }
        );

        base.OnModelCreating(modelBuilder);
    }


}