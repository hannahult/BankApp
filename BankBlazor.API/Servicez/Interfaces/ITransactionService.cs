using BankBlazor.API.DTOs;
using BankBlazor.API.Models;

namespace BankBlazor.API.Servicez.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionReadDTO>> GetTransactionsByAccountIdAsync(int accountId);
        Task<TransactionReadDTO> GetTransactionByIdAsync(int transactionId);
        Task<Transaction> CreateTransaction(TransactionCreateDTO transactionDto);

    }
}
