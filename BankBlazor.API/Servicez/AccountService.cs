using BankBlazor.API.Contexts;
using BankBlazor.API.DTOs;
using BankBlazor.API.Models;
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
        public async Task<PagedResult<AccountReadDTO>> GetAllAccountsAsync(int pageNumber, int pageSize)
        {
            var query = _dbContext.Accounts.OrderByDescending(a => a.Created);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AccountReadDTO
                {
                    AccountId = a.AccountId,
                    Balance = a.Balance,
                    Frequency = a.Frequency,
                    Created = a.Created
                })
                .ToListAsync();

            return new PagedResult<AccountReadDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
