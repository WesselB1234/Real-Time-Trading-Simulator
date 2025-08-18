using System.Text.Json.Serialization;

namespace RealTimeStockSimulator.Models
{
    public class TradablePriceInfos
    {
        [JsonInclude]
        public decimal Price;

        public TradablePriceInfos(decimal price)
        {
            Price = price;
        }
    }
}
