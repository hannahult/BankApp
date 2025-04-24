using BankBlazor.API.DTOs;
using BankBlazor.API.Models;
using BankBlazor.API.Servicez.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.API.Controllerz
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetTransactionsByAccountId(int accountId)
        {
            var transactions = await _transactionService.GetTransactionsByAccountIdAsync(accountId);
            if (transactions == null || !transactions.Any()) return NotFound();
            return Ok(transactions); 
        }

        [HttpGet("{transactionId}")]
        public async Task<IActionResult> GetTransactionById(int transactionId)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(transactionId);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }
        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateTransaction(TransactionCreateDTO transactionDto)
        {
            try
            {
                var newTransaction = await _transactionService.CreateTransaction(transactionDto);
                return CreatedAtAction(nameof(GetTransactionById), new { transactionId = newTransaction.TransactionId }, newTransaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
