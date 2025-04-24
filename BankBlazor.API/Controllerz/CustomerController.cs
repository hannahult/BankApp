using BankBlazor.API.DTOs;
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
    }
}
