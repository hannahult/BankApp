using BankBlazor.API.DTOs;
using BankBlazor.API.Models;

namespace BankBlazor.API.Servicez.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerReadDTO>> GetAllCustomersAsync();
        Task<CustomerReadDTO> GetCustomerByIdAsync(int id);
        //Task AddCustomerAsync(Customer customer);
        //Task UpdateCustomerAsync();
        //Task DeleteCustomerAsync(int id);
        Task<CustomerWithAccountsDTO?> GetCustomerWithAccountsAsync(int customerId);
    }
}
