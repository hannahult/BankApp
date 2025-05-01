using BankBlazor.API.Contexts;
using BankBlazor.API.DTOs;
using BankBlazor.API.Models;
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
        public async Task<CustomerWithAccountsDTO?> GetCustomerWithAccountsAsync(int customerId)
        {
            var customer = await _dbContext.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null) return null;

            return new CustomerWithAccountsDTO
            {
                CustomerId = customer.CustomerId,
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                Emailaddress = customer.Emailaddress,
                Streetaddress = customer.Streetaddress,
                City = customer.City,
                Country = customer.Country,
                Accounts = customer.Dispositions
                .Select(d => new AccountReadDTO
                {
                    AccountId = d.Account.AccountId,
                    Balance = d.Account.Balance,
                    Frequency = d.Account.Frequency,
                    Created = d.Account.Created.ToDateTime(TimeOnly.MinValue)
                }).ToList()
            };
        }
        public async Task<Customer> CreateCustomerAsync(CustomerCreateDTO dto)
        {
            var newCustomer = new Customer
            {
                Gender = dto.Gender,
                Givenname = dto.Givenname,
                Surname = dto.Surname,
                Streetaddress = dto.Streetaddress,
                City = dto.City,
                Zipcode = dto.Zipcode,
                Country = dto.Country,
                CountryCode = dto.CountryCode,
                Birthday = dto.Birthday,
                NationalId = dto.NationalId,
                Telephonecountrycode = dto.Telephonecountrycode,
                Telephonenumber = dto.Telephonenumber,
                Emailaddress = dto.Emailaddress
            };

            _dbContext.Customers.Add(newCustomer);
            await _dbContext.SaveChangesAsync();
            return newCustomer;
        }
        public async Task<PagedResult<CustomerReadDTO>> GetCustomersPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbContext.Customers.OrderBy(c => c.CustomerId);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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

            return new PagedResult<CustomerReadDTO>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
