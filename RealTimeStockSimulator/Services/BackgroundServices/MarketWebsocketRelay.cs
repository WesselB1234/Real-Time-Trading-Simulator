
using Microsoft.AspNetCore.SignalR;
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

        public MarketWebsocketRelay(IConfiguration configuration, IHubContext<MarketHub> hubContext)
        {
            _marketApiKey = configuration.GetValue<string>("ApiKeyStrings:MarketApiKey");
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ClientWebSocket client = new ClientWebSocket();
            Uri uri = new Uri($"wss://ws.finnhub.io?token={_marketApiKey}");

            try
            {
                await client.ConnectAsync(uri, stoppingToken);

                var subscribeRequest = new MarketSubscriptionRequest("subscribe", "BINANCE:BTCUSDT");
                string requestJson = JsonSerializer.Serialize(subscribeRequest, _jsonSerializerOptions);

                await client.SendAsync(
                    Encoding.UTF8.GetBytes(requestJson),
                    WebSocketMessageType.Text,
                    true,
                    stoppingToken
                );

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
