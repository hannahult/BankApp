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
            if (!transactions.Any()) return NotFound();
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

        [HttpPost("transfer")]
        public async Task<IActionResult> MakeTransfer([FromBody] TransferDTO transferDto)
        {
            try
            {
                await _transactionService.TransferAsync(transferDto);
                return Ok("Transfer successful!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetTransactionsByCustomerId(int customerId)
        {
            var transactions = await _transactionService.GetTransactionsByCustomerIdAsync(customerId);
            if (!transactions.Any()) return NotFound("No transactions found for this customer.");
            return Ok(transactions);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<TransactionReadDTO>>> GetPagedTransactions(int pageNumber = 1, int pageSize = 20)
        {
            var result = await _transactionService.GetTransactionsPagedAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("customer/{customerId}/paged")]
        public async Task<ActionResult<PagedResult<TransactionReadDTO>>> GetCustomerTransactionsPaged(int customerId, int pageNumber = 1, int pageSize = 20)
        {
            var result = await _transactionService.GetCustomerTransactionsPagedAsync(customerId, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("account/{accountId}/paged")]
        public async Task<ActionResult<PagedResult<TransactionReadDTO>>> GetAccountTransactionsPaged(int accountId, int pageNumber = 1, int pageSize = 20)
        {
            var result = await _transactionService.GetAccountTransactionsPagedAsync(accountId, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost("deposit")]
        public async Task<ActionResult<Transaction>> Deposit(TransactionCreateDTO dto) 
        {
            try
            {
                var transaction = await _transactionService.WithdrawAsync(dto);
                return CreatedAtAction(nameof(GetTransactionById), new { transactionId = transaction.TransactionId }, transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult<Transaction>> Withdraw(TransactionCreateDTO dto) 
        {

            try
            {
                var transaction = await _transactionService.WithdrawAsync(dto);
                return CreatedAtAction(nameof(GetTransactionById), new { transactionId = transaction.TransactionId }, transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
