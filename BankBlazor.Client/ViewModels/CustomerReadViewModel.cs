namespace BankBlazor.Client.ViewModels
{
    public class CustomerReadViewModel
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Emailaddress { get; set; }
        public string Telephonenumber { get; set; }
        public string Streetaddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        
    }
}
