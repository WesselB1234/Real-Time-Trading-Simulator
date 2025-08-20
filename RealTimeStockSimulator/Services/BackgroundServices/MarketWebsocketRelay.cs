
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using RealTimeStockSimulator.Hubs;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Models.Interfaces;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace RealTimeStockSimulator.Services.BackgroundServices
{
    public class MarketWebsocketRelay : BackgroundService
    {   
        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private string? _marketApiKey;
        private IHubContext<MarketHub> _hubContext;
        private IMemoryCache _memoryCache;
        private Dictionary<string, TradablePriceInfos> _tradablePriceInfosDictionary;
        private IStringFormatter _stringFormatter;

        public MarketWebsocketRelay(IConfiguration configuration, IHubContext<MarketHub> hubContext, IMemoryCache memoryCache, IStringFormatter stringFormatter)
        {
            _marketApiKey = configuration.GetValue<string>("ApiKeyStrings:MarketApiKey");
            _hubContext = hubContext;
            _memoryCache = memoryCache;
            _stringFormatter = stringFormatter;
        }

        private async Task SubscribeToTradablesInCache(ClientWebSocket client)
        {
            foreach (KeyValuePair<string, TradablePriceInfos> entry in _tradablePriceInfosDictionary)
            {
                var subscribeRequest = new MarketSubscriptionRequest("subscribe", entry.Key);
                string requestJson = JsonSerializer.Serialize(subscribeRequest, _jsonSerializerOptions);

                await client.SendAsync(Encoding.UTF8.GetBytes(requestJson), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        private async Task HandleMarketWebSocketPayload(MarketWebsocketPayload marketPayload)
        {
            MarketWebsocketTradable responseTradable = marketPayload.Data[marketPayload.Data.Count - 1];
            TradablePriceInfos tradablePriceInfos = _tradablePriceInfosDictionary[responseTradable.Symbol];

            if (responseTradable.Price != null && tradablePriceInfos.Price != responseTradable.Price)
            {
                tradablePriceInfos.Price = (decimal)responseTradable.Price;
                tradablePriceInfos.FormattedPrice = _stringFormatter.FormatDecimalPrice(tradablePriceInfos.Price);
                TradableUpdatePayload tradableUpdatePayload = new TradableUpdatePayload(responseTradable.Symbol, tradablePriceInfos);

                await _hubContext.Clients.All.SendAsync("ReceiveMarketData", JsonSerializer.Serialize(tradableUpdatePayload), CancellationToken.None);
            }
        }

        private Dictionary<string, TradablePriceInfos> GetTradablePriceInfosDictionary()
        {
            if (_memoryCache.Get("TradablePriceInfosDictionary") is Dictionary<string, TradablePriceInfos> tradablePriceInfosDictionary)
            {
                return tradablePriceInfosDictionary;
            }
            else
            {
                throw new Exception("TradablesPriceInfosDictionary does not exist in cache.");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            ClientWebSocket client = new ClientWebSocket();

            try
            {
                _tradablePriceInfosDictionary = GetTradablePriceInfosDictionary();

                Uri uri = new Uri($"wss://ws.finnhub.io?token={_marketApiKey}");

                await client.ConnectAsync(uri, CancellationToken.None);
                await SubscribeToTradablesInCache(client);

                byte[] buffer = new byte[4096];
 
                while (!cancellationToken.IsCancellationRequested && client.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        break;
                    }

                    string json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    MarketWebsocketPayload? marketPayload = JsonSerializer.Deserialize<MarketWebsocketPayload>(json);

                    if (marketPayload != null && marketPayload.Type == "trade")
                    {
                        await HandleMarketWebSocketPayload(marketPayload);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Market websocket error: {ex.Message}");
            }

            try
            {
                if (client.State == WebSocketState.Open)
                {
                    await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Session completed!", CancellationToken.None);
                    Console.WriteLine("Successfully closed the market websocket.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Market websocket closing error: {ex.Message}");
            }

            client.Dispose();
        }
    }
}
