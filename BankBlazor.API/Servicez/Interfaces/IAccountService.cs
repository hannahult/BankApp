using BankBlazor.API.DTOs;

namespace BankBlazor.API.Servicez.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountReadDTO>> GetAccountsByCustomerIdAsync(int customerId);
        Task<AccountReadDTO> GetAccountByIdAsync(int accountId);
        Task<List<AccountReadDTO>> GetAllAccountsAsync();
    }
}
