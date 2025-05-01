using BankBlazor.API.Contexts;
using BankBlazor.API.DTOs;
using BankBlazor.API.Servicez.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.API.Servicez
{
    public class AccountService : IAccountService
    {
        private readonly BankContext _dbContext;

        public AccountService(BankContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AccountReadDTO>> GetAccountsByCustomerIdAsync(int customerId)
        {
            var accounts = await _dbContext.Dispositions
                .Where(d => d.CustomerId == customerId)
                .Include(d => d.Account)
                .Select(d => new AccountReadDTO
                {
                    AccountId = d.Account.AccountId,
                    Frequency = d.Account.Frequency,
                    Balance = d.Account.Balance
                })
                .ToListAsync();

            return accounts;
        }
        public async Task<AccountReadDTO> GetAccountByIdAsync(int accountId)
        {
            var account = await _dbContext.Accounts
                .Where(a => a.AccountId == accountId)
                .Select(a => new AccountReadDTO
                {
                    AccountId = a.AccountId,
                    Frequency = a.Frequency,
                    Balance = a.Balance
                })
                .FirstOrDefaultAsync();

            return account;
        }
        public async Task<List<AccountReadDTO>> GetAllAccountsAsync()
        {
            return await _dbContext.Accounts
                .OrderByDescending(a => a.Balance)
                .Select(a => new AccountReadDTO
                {
                    AccountId = a.AccountId,
                    Balance = a.Balance,
                    Created = a.Created,
                    Frequency = a.Frequency
                })
                .ToListAsync();
        }
    }
}
