using System.Text.Json.Serialization;

namespace RealTimeStockSimulator.Models
{
    public class IncomingMarketWebsocketTradable
    {
        [JsonPropertyName("s")]
        public string? Symbol { get; set; }
        [JsonPropertyName("p")]
        public decimal? Price { get; set; }

        public IncomingMarketWebsocketTradable()
        {

        }

        public IncomingMarketWebsocketTradable(string? symbol, decimal? price)
        {
            Symbol = symbol;
            Price = price;
        }
    }
}
