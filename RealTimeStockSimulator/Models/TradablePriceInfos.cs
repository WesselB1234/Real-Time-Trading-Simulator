using System.Text.Json.Serialization;

namespace RealTimeStockSimulator.Models
{
    public class TradablePriceInfos
    {
        [JsonInclude]
        public decimal Price { get; set; }

        [JsonPropertyName("c")]
        public decimal PriceAlias {
            get
            {
                return Price;
            }
            set
            {
                Price = value;
            } 
        }

        public string? FormattedPrice { get; set; }

        public TradablePriceInfos(decimal price)
        {
            Price = price;
        }

        public TradablePriceInfos()
        {

        }
    }
}
