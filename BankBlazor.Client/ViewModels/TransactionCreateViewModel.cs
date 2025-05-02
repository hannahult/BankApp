namespace BankBlazor.Client.ViewModels
{
    public class TransactionCreateViewModel
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string? Symbol { get; set; }
        public string? Bank { get; set; }
        public string? Account { get; set; }
    }
}
