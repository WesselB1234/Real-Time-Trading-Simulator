using System.Text.Json.Serialization;

namespace RealTimeStockSimulator.Models
{
    public class MarketWebsocketPayload
    {
        [JsonPropertyName("data")]
        public List<Tradable> Data { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
