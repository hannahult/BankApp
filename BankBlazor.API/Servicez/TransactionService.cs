using BankBlazor.API.Contexts;
using BankBlazor.API.DTOs;
using BankBlazor.API.Models;
using BankBlazor.API.Servicez.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                    Date = t.Date,
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
                    Date = t.Date,
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
    }
}
