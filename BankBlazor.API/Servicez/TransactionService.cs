using BankBlazor.API.Contexts;
using BankBlazor.API.DTOs;
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
    }
}
