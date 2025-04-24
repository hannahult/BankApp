﻿namespace BankBlazor.API.DTOs
{
    public class TransactionCreateDTO
    {
        public int AccountId { get; set; }
        public DateOnly Date { get; set; }
        public string Type { get; set; } = null!;
        public string Operation { get; set; } = null!;
        public decimal Amount { get; set; }
        public string? Symbol { get; set; }
        public string? Bank { get; set; }
        public string? Account { get; set; }
    }
}
