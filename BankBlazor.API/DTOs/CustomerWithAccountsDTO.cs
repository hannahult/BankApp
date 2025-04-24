namespace BankBlazor.API.DTOs
{
    public class CustomerWithAccountsDTO
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public List<AccountReadDTO> Accounts { get; set; } = new();
    }
}
