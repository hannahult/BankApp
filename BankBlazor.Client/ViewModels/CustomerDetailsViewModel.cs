namespace BankBlazor.Client.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public List<AccountReadViewModel> Accounts { get; set; } = new();
    }
}
