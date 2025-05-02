using BankBlazor.Shared.DTOs;
using BankBlazor.Shared.Models;

namespace BankApp.Shared.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerReadDTO>> GetAllCustomersAsync();
        Task<CustomerReadDTO> GetCustomerByIdAsync(int customerId);
        Task<CustomerWithAccountsDTO?> GetCustomerWithAccountsAsync(int customerId);
        Task<PagedResult<CustomerReadDTO>> GetCustomersPagedAsync(int pageNumber, int pageSize);
    }
}
