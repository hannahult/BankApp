using BankBlazor.API.DTOs;
using BankBlazor.API.Models;

namespace BankBlazor.API.Servicez.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountReadDTO>> GetAccountsByCustomerIdAsync(int customerId);
        Task<AccountReadDTO> GetAccountByIdAsync(int accountId);
        Task<PagedResult<AccountReadDTO>> GetAccountsPagedAsync(int pageNumber, int pageSize);
    }
}
