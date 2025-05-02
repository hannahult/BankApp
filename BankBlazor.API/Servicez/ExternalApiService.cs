using BankBlazor.API.Models;
using Newtonsoft.Json;

namespace BankBlazor.API.Servicez
{
    public class ExternalApiService
    {
        private readonly HttpClient _http;

        public ExternalApiService(HttpClient http)
        {
            _http = http;
        }
        public async Task<Holiday?> GetNextScottishHolidayAsync()
        {
            var url = "https://www.gov.uk/bank-holidays.json";
            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<Dictionary<string, Region>>(content);

            if (!data.TryGetValue("scotland", out var scotland))
                return null;

            return scotland.Events
                .Where(e => e.Date > DateTime.Today)
                .OrderBy(e => e.Date)
                .FirstOrDefault();
        }
    }
}
