using BankBlazor.API.DTOs;

namespace BankBlazor.API.Servicez.Interfaces
{
    public interface ITransactionService
    {
        public interface ITransactionService
        {
            Task<List<TransactionReadDTO>> GetTransactionsByAccountIdAsync(int accountId);
            Task<TransactionReadDTO> GetTransactionByIdAsync(int transactionId);
        }
    }
}
