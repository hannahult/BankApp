using BankBlazor.API.Models;

namespace BankBlazor.API.Servicez.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }
}
