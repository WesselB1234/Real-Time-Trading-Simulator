
using RealTimeStockSimulator.Models;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace RealTimeStockSimulator.BackgroundServices
{
    public class MarketWebsocketRelay : BackgroundService
    {   
        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly string _stockMarketApiKey;

        public MarketWebsocketRelay(IConfiguration configuration)
        {
            _stockMarketApiKey = configuration.GetValue<string>("ApiKeyStrings:StockMarketApiKey");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ClientWebSocket client = new ClientWebSocket();
            Uri uri = new Uri($"wss://ws.finnhub.io?token={_stockMarketApiKey}");

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
                    Console.WriteLine(message);
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
