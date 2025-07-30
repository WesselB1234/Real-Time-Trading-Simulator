
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using RealTimeStockSimulator.Hubs;
using RealTimeStockSimulator.Models;
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

        public MarketWebsocketRelay(IConfiguration configuration, IHubContext<MarketHub> hubContext, IMemoryCache memoryCache)
        {
            _marketApiKey = configuration.GetValue<string>("ApiKeyStrings:MarketApiKey");
            _hubContext = hubContext;
            _memoryCache = memoryCache;
        }

        private async Task SubscribeToTradablesInCache(ClientWebSocket client, CancellationToken stoppingToken)
        {
            Dictionary<string, Tradable>? tradablesDictionary = (Dictionary<string, Tradable>?)_memoryCache.Get("TradablesDictionary");

            if (tradablesDictionary == null)
            {
                return;
            }

            foreach (KeyValuePair<string, Tradable> entry in tradablesDictionary)
            {
                var subscribeRequest = new MarketSubscriptionRequest("subscribe", entry.Value.Symbol);
                string requestJson = JsonSerializer.Serialize(subscribeRequest, _jsonSerializerOptions);

                await client.SendAsync(
                    Encoding.UTF8.GetBytes(requestJson),
                    WebSocketMessageType.Text,
                    true,
                    stoppingToken
                );
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ClientWebSocket client = new ClientWebSocket();
            Uri uri = new Uri($"wss://ws.finnhub.io?token={_marketApiKey}");

            try
            {
                await client.ConnectAsync(uri, stoppingToken);
                await SubscribeToTradablesInCache(client, stoppingToken);

                byte[] buffer = new byte[4096];

                while (!stoppingToken.IsCancellationRequested && client.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), stoppingToken);

                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    await _hubContext.Clients.All.SendAsync("ReceiveMarketData", message);
                }
            }
            catch (OperationCanceledException)
            {
                // Expected when stoppingToken is cancelled — graceful shutdown
            }
            catch (WebSocketException ex)
            {
                Console.WriteLine($"WebSocket error: {ex.Message}");
            }
            finally
            {
                if (client.State == WebSocketState.Open || client.State == WebSocketState.CloseReceived)
                {
                    try
                    {
                        await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Session completed!", CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error closing WebSocket: {ex.Message}");
                    }
                }

                Console.WriteLine("WebSocket closed. Background service stopping.");
            }
        }
    }
}
