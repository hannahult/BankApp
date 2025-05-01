using BankBlazor.API.DTOs;
using BankBlazor.API.Models;

namespace BankBlazor.API.Servicez.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerReadDTO>> GetAllCustomersAsync();
        Task<CustomerReadDTO> GetCustomerByIdAsync(int customerId);
        Task<Customer> CreateCustomerAsync(CustomerCreateDTO dto);
        Task<CustomerWithAccountsDTO?> GetCustomerWithAccountsAsync(int customerId);
        Task<PagedResult<CustomerReadDTO>> GetCustomersPagedAsync(int pageNumber, int pageSize);
    }
}
