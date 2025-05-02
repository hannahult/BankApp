using Newtonsoft.Json;

namespace BankBlazor.API.Models
{
    public class Region
    {
        [JsonProperty("events")]
        public List<Holiday> Events { get; set; } = new();
    }
}
