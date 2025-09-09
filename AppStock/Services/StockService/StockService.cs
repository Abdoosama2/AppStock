using AppStock.Data;
using AppStock.Models;
using AppStock.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace AppStock.Services.StockService
{
    public class StockService : IStockService
    {

        AppDbContext _dbContext;

        public StockService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {

          
            if(buyOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(buyOrderRequest));
            }

            var buyOrder = new BuyOrder()
            {
                StockName=buyOrderRequest.StockName,
                StockPrice=buyOrderRequest.StockPrice,
                BuyOrderId=Guid.NewGuid(),
                Quantity=buyOrderRequest.Quantity,  
                DateAndTimeOfOrder=buyOrderRequest.DateAndTimeOfOrder,
                StockSymbol=buyOrderRequest.StockSymbol,

            };

            _dbContext.BuyOrders.Add(buyOrder);
            await _dbContext.SaveChangesAsync();

            return new BuyOrderResponse()
            {
                StockName= buyOrder.StockName,
                StockPrice=buyOrder.StockPrice,
                Quantity=buyOrder.Quantity,
                BuyOrderId = buyOrder.BuyOrderId,
                DateAndTimeOfOrder= buyOrder.DateAndTimeOfOrder,
                StockSymbol=buyOrder.StockSymbol,


            };

        }

        public  async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
            {
                throw new ArgumentNullException(nameof(sellOrderRequest));
            }

            var sellOrder = new SellOrder()
            {
                StockName = sellOrderRequest.StockName,
                StockPrice = sellOrderRequest.StockPrice,
                SellOrderId = Guid.NewGuid(),
                Quantity = sellOrderRequest.Quantity,
                DateAndTimeOfOrder = sellOrderRequest.DateAndTimeOfOrder,
                StockSymbol = sellOrderRequest.StockSymbol,

            };

            _dbContext.SellOrders.Add(sellOrder);
            await _dbContext.SaveChangesAsync();

            return new SellOrderResponse()
            {
                StockName = sellOrder.StockName,
                StockPrice = sellOrder.StockPrice,
                Quantity = sellOrder.Quantity,
                SellOrderId = sellOrder.SellOrderId,
                DateAndTimeOfOrder = sellOrder.DateAndTimeOfOrder,
                StockSymbol = sellOrder.StockSymbol,


            };
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return await _dbContext.BuyOrders
            .Select(b => new BuyOrderResponse
            {
                StockName = b.StockName,
                StockPrice = b.StockPrice,
                Quantity = b.Quantity,
                BuyOrderId = b.BuyOrderId,
                DateAndTimeOfOrder = b.DateAndTimeOfOrder,
                StockSymbol = b.StockSymbol,
            })
            .ToListAsync();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            return await _dbContext.SellOrders.Select(b => new SellOrderResponse
            {
                StockName = b.StockName,
                StockPrice = b.StockPrice,
                Quantity = b.Quantity,
                SellOrderId = b.SellOrderId,
                DateAndTimeOfOrder = b.DateAndTimeOfOrder,
                StockSymbol = b.StockSymbol,


            }).ToListAsync();
        }
    }
}
