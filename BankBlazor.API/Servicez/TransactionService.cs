using BankBlazor.API.Contexts;
using BankBlazor.API.Controllerz;
using BankBlazor.API.DTOs;
using BankBlazor.API.Models;
using BankBlazor.API.Servicez.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BankBlazor.API.Servicez
{
    public class TransactionService : ITransactionService
    {
        private readonly BankContext _dbContext;

        public TransactionService(BankContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TransactionReadDTO>> GetTransactionsByAccountIdAsync(int accountId)
        {
            var transactions = await _dbContext.Transactions
                .Where(t => t.AccountId == accountId)
                .Select(t => new TransactionReadDTO
                {
                    TransactionId = t.TransactionId,
                    Date = t.Date.ToDateTime(TimeOnly.MinValue),
                    Type = t.Type,
                    Operation = t.Operation,
                    Amount = t.Amount,
                    Balance = t.Balance,
                    Symbol = t.Symbol,
                    Bank = t.Bank,
                    Account = t.Account
                })
                .ToListAsync();
            return transactions;
        }
        public async Task<TransactionReadDTO> GetTransactionByIdAsync(int transactionId)
        {
            var transaction = await _dbContext.Transactions
                .Where(t => t.TransactionId == transactionId)
                .Select(t => new TransactionReadDTO
                {
                    TransactionId = t.TransactionId,
                    Date = t.Date.ToDateTime(TimeOnly.MinValue),
                    Type = t.Type,
                    Operation = t.Operation,
                    Amount = t.Amount,
                    Balance = t.Balance,
                    Symbol = t.Symbol,
                    Bank = t.Bank,
                    Account = t.Account
                })
                .FirstOrDefaultAsync();

            return transaction;
        }
        public async Task<Transaction> CreateTransaction(TransactionCreateDTO transactionDto)
        {
            var account = await _dbContext.Accounts.FindAsync(transactionDto.AccountId);
            if (account == null)
                throw new Exception("Account not found");

            var transaction = new Transaction
            {
                AccountId = transactionDto.AccountId,
                Date = transactionDto.Date,
                Type = transactionDto.Type,
                Operation = transactionDto.Operation,
                Amount = transactionDto.Amount,
                Balance = account.Balance + transactionDto.Amount,
                Symbol = transactionDto.Symbol,
                Bank = transactionDto.Bank,
                Account = transactionDto.Account
            };

            account.Balance = transaction.Balance;

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            return transaction;
        }

        public async Task TransferAsync(TransferDTO transferDto)
        {
            var fromAccount = await _dbContext.Accounts.FindAsync(transferDto.FromAccountId);
            var toAccount = await _dbContext.Accounts.FindAsync(transferDto.ToAccountId);

            if (fromAccount == null || toAccount == null)
                throw new Exception("One or both accounts not found");

            if (fromAccount.Balance < transferDto.Amount)
                throw new Exception("Insufficient funds");

            fromAccount.Balance -= transferDto.Amount;

            toAccount.Balance += transferDto.Amount;

            var withdrawal = new Transaction
            {
                AccountId = fromAccount.AccountId,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Type = "Debit",
                Operation = "Transfer Out",
                Amount = -transferDto.Amount,
                Balance = fromAccount.Balance,
                Symbol = transferDto.Symbol,
                Bank = transferDto.Bank,
                Account = transferDto.Account
            };

            var deposit = new Transaction
            {
                AccountId = toAccount.AccountId,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Type = "Credit",
                Operation = "Transfer In",
                Amount = transferDto.Amount,
                Balance = toAccount.Balance,
                Symbol = transferDto.Symbol,
                Bank = transferDto.Bank,
                Account = transferDto.Account
            };

            _dbContext.Transactions.Add(withdrawal);
            _dbContext.Transactions.Add(deposit);

            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<TransactionReadDTO>> GetTransactionsByCustomerIdAsync(int customerId)
        {
            var transactions = await _dbContext.Transactions
                .Include(t => t.AccountNavigation)
                .Where(t => t.AccountNavigation.Dispositions.Any(d => d.CustomerId == customerId))
                .OrderByDescending(t => t.Date)
                .Select(t => new TransactionReadDTO
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Date = t.Date.ToDateTime(TimeOnly.MinValue),
                    Type = t.Type,
                    Operation = t.Operation,
                    Amount = t.Amount,
                    Balance = t.Balance,
                    Symbol = t.Symbol,
                    Bank = t.Bank,
                    Account = t.Account
                })
                .ToListAsync();

            return transactions;
        }
        public async Task<List<TransactionReadDTO>> GetAllTransactionsAsync()
        {
            Console.WriteLine("Loading first 500 transactions only for performance");

            return await _dbContext.Transactions
                .OrderByDescending(t => t.Date)
                .Take(500) // 👈 Begränsa till 500 för testning
                .Select(t => new TransactionReadDTO
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Date = t.Date.ToDateTime(TimeOnly.MinValue),
                    Type = t.Type,
                    Operation = t.Operation,
                    Amount = t.Amount,
                    Balance = t.Balance,
                    Symbol = t.Symbol,
                    Bank = t.Bank,
                    Account = t.Account
                })
                .ToListAsync();
        }

        public async Task<PagedResult<TransactionReadDTO>> GetTransactionsPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbContext.Transactions
                .OrderByDescending(t => t.Date);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TransactionReadDTO
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Date = t.Date.ToDateTime(TimeOnly.MinValue),
                    Type = t.Type,
                    Operation = t.Operation,
                    Amount = t.Amount,
                    Balance = t.Balance,
                    Symbol = t.Symbol,
                    Bank = t.Bank,
                    Account = t.Account
                })
                .ToListAsync();

            return new PagedResult<TransactionReadDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<PagedResult<TransactionReadDTO>> GetCustomerTransactionsPagedAsync(int customerId, int pageNumber, int pageSize)
        {
            var query = _dbContext.Transactions
                .Where(t => t.AccountNavigation.Dispositions.Any(d => d.CustomerId == customerId))
                .OrderByDescending(t => t.Date);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TransactionReadDTO
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Date = t.Date.ToDateTime(TimeOnly.MinValue),
                    Type = t.Type,
                    Operation = t.Operation,
                    Amount = t.Amount,
                    Balance = t.Balance,
                    Symbol = t.Symbol,
                    Bank = t.Bank,
                    Account = t.Account
                })
                .ToListAsync();

            return new PagedResult<TransactionReadDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
