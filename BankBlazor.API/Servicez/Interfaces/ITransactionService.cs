using BankBlazor.API.DTOs;
using BankBlazor.API.Models;

namespace BankBlazor.API.Servicez.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionReadDTO>> GetTransactionsByAccountIdAsync(int accountId);
        Task<TransactionReadDTO> GetTransactionByIdAsync(int transactionId);
        Task<Transaction> CreateTransaction(TransactionCreateDTO transactionDto);
        Task TransferAsync(TransferDTO transferDto);
        Task<List<TransactionReadDTO>> GetTransactionsByCustomerIdAsync(int customerId);
        Task<List<TransactionReadDTO>> GetAllTransactionsAsync();
        Task<PagedResult<TransactionReadDTO>> GetTransactionsPagedAsync(int pageNumber, int pageSize);
        Task<PagedResult<TransactionReadDTO>> GetCustomerTransactionsPagedAsync(int customerId, int pageNumber, int pageSize);
        Task<PagedResult<TransactionReadDTO>> GetAccountTransactionsPagedAsync(int accountId, int pageNumber, int pageSize);
        Task<Transaction> DepositAsync(TransactionCreateDTO transactionDto);
        Task<Transaction> WithdrawAsync(TransactionCreateDTO transactionDto);
    }
}
