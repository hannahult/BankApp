namespace BankBlazor.Client.ViewModels
{
    public class AccountReadViewModel
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public decimal Balance { get; set; }
        public DateOnly Created { get; set; }
    }
}
