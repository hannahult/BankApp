namespace BankBlazor.API.DTOs
{
    public class CustomerUpdateDTO
    {
        public string Givenname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Emailaddress { get; set; }
        public string? Telephonenumber { get; set; }
        public string Streetaddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
