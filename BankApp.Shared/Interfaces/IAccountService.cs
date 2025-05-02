using BankBlazor.Shared.DTOs;
using BankBlazor.Shared.Models;

namespace BankApp.Shared.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountReadDTO>> GetAccountsByCustomerIdAsync(int customerId);
        Task<AccountReadDTO> GetAccountByIdAsync(int accountId);
        Task<PagedResult<AccountReadDTO>> GetAllAccountsAsync(int pageNumber, int pageSize);
    }
}
