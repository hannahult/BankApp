﻿namespace BankBlazor.Client.ViewModels
{
    public class TransactionReadViewModel
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public DateOnly Date { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Operation { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string? Symbol { get; set; }
        public string? Bank { get; set; }
        public string? Account { get; set; }
    }
}
