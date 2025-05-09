﻿using BankBlazor.Shared.DTOs;
using BankBlazor.Shared.Models;

namespace BankApp.Shared.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionReadDTO>> GetTransactionsByAccountIdAsync(int accountId);
        Task<TransactionReadDTO> GetTransactionByIdAsync(int transactionId);
        Task<TransactionReadDTO> CreateTransaction(TransactionCreateDTO transactionDto);
        Task TransferAsync(TransferDTO transferDto);
        Task<List<TransactionReadDTO>> GetTransactionsByCustomerIdAsync(int customerId);
        Task<List<TransactionReadDTO>> GetAllTransactionsAsync();
        Task<PagedResult<TransactionReadDTO>> GetTransactionsPagedAsync(int pageNumber, int pageSize);
        Task<PagedResult<TransactionReadDTO>> GetCustomerTransactionsPagedAsync(int customerId, int pageNumber, int pageSize);
        Task<PagedResult<TransactionReadDTO>> GetAccountTransactionsPagedAsync(int accountId, int pageNumber, int pageSize);
        Task<TransactionReadDTO> DepositAsync(TransactionCreateDTO transactionDto);
        Task<TransactionReadDTO> WithdrawAsync(TransactionCreateDTO transactionDto);
    }
}
