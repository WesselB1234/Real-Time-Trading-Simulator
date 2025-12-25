using System.Text.Json.Serialization;

namespace RealTimeStockSimulator.Models
{
    public class IncomingMarketWebsocketPayload
    {
        [JsonPropertyName("data")]
        public List<IncomingMarketWebsocketTradable> Data { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        public IncomingMarketWebsocketPayload(List<IncomingMarketWebsocketTradable> data, string type)
        {
            Data = data;
            Type = type;
        }
    }
}
