
using Microsoft.AspNetCore.SignalR;
using RealTimeStockSimulator.Hubs;

namespace RealTimeStockSimulator.Services.HostedServices
{
    public class ApiCacheInitializer : IHostedService
    {
        private string? _marketApiKey;

        public ApiCacheInitializer(IConfiguration configuration)
        {
            _marketApiKey = configuration.GetValue<string>("ApiKeyStrings:MarketApiKey");
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://finnhub.io/api/v1/quote?symbol=AAPL&token={_marketApiKey}", cancellationToken);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(content);
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Request error: {ex.Message}");
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
