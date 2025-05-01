using BankBlazor.API.DTOs;
using BankBlazor.API.Models;
using BankBlazor.API.Servicez.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.API.Controllerz
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetAccountsByCustomerId(int customerId)
        {
            var accounts = await _accountService.GetAccountsByCustomerIdAsync(customerId);
            if (!accounts.Any()) return NotFound();
            return Ok(accounts);
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccountById(int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<AccountReadDTO>>> GetPagedAccounts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _accountService.GetAllAccountsAsync(pageNumber, pageSize);
            return Ok(result);
        }
    }
}
