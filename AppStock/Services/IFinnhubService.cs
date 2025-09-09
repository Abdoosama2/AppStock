namespace AppStock.Services
{
    public interface IFinnhubService
    {

        public Task<Dictionary<string, object>> GetCompanyProfile(string StockSymbol);

        public Task<Dictionary <string, object>> GetStockPriceQuote( string StockSymbol);

    }
}
