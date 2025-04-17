namespace BankBlazor.API.DTOs
{
    public class AccountReadDTO
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public decimal Balance { get; set; }
    }
}
