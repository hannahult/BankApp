using Newtonsoft.Json;

namespace BankBlazor.API.Models
{
    public class Holiday
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public bool Bunting { get; set; }
    }
}
