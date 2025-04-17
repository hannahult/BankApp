using BankBlazor.API.Contexts;
using BankBlazor.API.DTOs;
using BankBlazor.API.Servicez.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.API.Servicez
{
    public class CustomerService : ICustomerService
    {
        private readonly BankContext _dbContext;

        public CustomerService(BankContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CustomerReadDTO>> GetAllCustomersAsync()
        {
            return await _dbContext.Customers
                .Select(c => new CustomerReadDTO
                {
                    CustomerId = c.CustomerId,
                    Givenname = c.Givenname,
                    Surname = c.Surname,
                    Emailaddress = c.Emailaddress,
                    Telephonenumber = c.Telephonenumber,
                    Streetaddress = c.Streetaddress,
                    City = c.City,
                    Country = c.Country
                })
                .ToListAsync();
        }

        public async Task<CustomerReadDTO> GetCustomerByIdAsync(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);

            if (customer == null)
                throw new Exception("Customer not found");

            return new CustomerReadDTO
            {
                CustomerId = customer.CustomerId,
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                Emailaddress = customer.Emailaddress,
                Telephonenumber = customer.Telephonenumber,
                Streetaddress = customer.Streetaddress,
                City = customer.City,
                Country = customer.Country
            };
        }

    }
}
