using AppStock.Models.ViewModle;
using AppStock.Services;
using AppStock.Services.StockService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AppStock.Controllers
{
    public class TradeController : Controller
    {
        private IFinnhubService _finnhubService;
        private IStockService _stockservice;
        private readonly TradingOptions _tradingOptions;

        public TradeController(IFinnhubService finnhubService, IStockService stockservice, IOptions<TradingOptions> tradingOptions)
        {
            _finnhubService = finnhubService;
            _stockservice = stockservice;
            _tradingOptions = tradingOptions.Value;
        }

        [HttpGet]
        
        public async Task<IActionResult> Index(string stockSymbol)
        {
            // 1. Get default stock symbol from appsettings.json
           // string stockSymbol = _tradingOptions.DefaultStockSymbol ?? "MSFT";

            // 2. Fetch company profile from Finnhub
            var profile = await _finnhubService.GetCompanyProfile(stockSymbol);

            // 3. Fetch stock quote (price) from Finnhub
            var quote = await _finnhubService.GetStockPriceQuote(stockSymbol);

            // 4. Build StockTrade model
            var stockTrade = new StockTrade
            {
                StockSymbol = profile != null && profile.ContainsKey("ticker")
                              ? profile["ticker"]?.ToString()
                              : stockSymbol,
                StockName = profile != null && profile.ContainsKey("name")
                              ? profile["name"]?.ToString()
                              : "Unknown",
                Price = quote != null && quote.ContainsKey("c")
        ? Convert.ToDouble(((JsonElement)quote["c"]).GetDouble())
        : 0.0,

                Quantity = 0 // default quantity
            };

            // 5. Send model to view
            return View(stockTrade);

        }

        [HttpPost]

        public IActionResult Search(string stockSymbol)
        {
            return RedirectToAction("Index",new { stockSymbol });
        }
    }
}
