using System.Text.Json.Serialization;

namespace RealTimeStockSimulator.Models
{
    public class Tradable
    {
        public string Symbol { get; set; }
        [JsonPropertyName("c")]
        public decimal? Price { get; set; }
        [JsonPropertyName("p")]
        public decimal? PriceFromWebsocket {
            get
            {
                return Price;
            }
            set 
            { 
                Price = value;
            } 
        }

        public Tradable(string symbol) 
        { 
            Symbol = symbol;
        }
    }
}
