
using System.Text.Json;

namespace AppStock.Services
{
    public class FinnhubService : IFinnhubService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiToken;


        public FinnhubService(HttpClient httpClient,IConfiguration config)
        {
            _httpClient = httpClient;

            _apiToken = config["FinnhubToken"]
                        ?? throw new System.Exception("Finnhub token missing from configuration.");
        }

       
      
        public async Task<Dictionary<string, object>?> GetCompanyProfile(string StockSymbol)
        {

            var url = $"https://finnhub.io/api/v1/stock/profile2?symbol={StockSymbol}&token={_apiToken}";
            var response=await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;

            }
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string StockSymbol)
        {
            var url = $"https://finnhub.io/api/v1/quote?symbol={StockSymbol}&token={_apiToken}";
            var response= await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Dictionary<string,object>>(json);
        }
    }
}
