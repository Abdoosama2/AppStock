namespace AppStock.Models.DTO
{
    public class BuyOrderResponse
    {

        public Guid BuyOrderId { get; set; }
        public string StockSymbol { get; set; }

        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }


        public double StockPrice { get; set; }
    }
}
