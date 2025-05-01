using BankBlazor.API.DTOs;
using BankBlazor.API.Models;
using BankBlazor.API.Servicez;
using BankBlazor.API.Servicez.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.API.Controllerz
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<ActionResult<List<CustomerReadDTO>>> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpGet("{customerId}/accounts")]
        public async Task<IActionResult> GetCustomerWithAccounts(int customerId)
        {
            var customer = await _customerService.GetCustomerWithAccountsAsync(customerId);
            if (customer == null) return NotFound();
            return Ok(customer);
        }
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<CustomerReadDTO>>> GetPagedCustomers(int pageNumber = 1, int pageSize = 50)
        {
            var result = await _customerService.GetCustomersPagedAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateDTO dto)
        {
            try
            {
                var newCustomer = await _customerService.CreateCustomerAsync(dto);
                return CreatedAtAction(nameof(GetCustomerById), new { customerId = newCustomer.CustomerId }, newCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
