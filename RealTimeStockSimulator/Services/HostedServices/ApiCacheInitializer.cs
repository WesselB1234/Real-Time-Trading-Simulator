
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using RealTimeStockSimulator.Hubs;

namespace RealTimeStockSimulator.Services.HostedServices
{
    public class ApiCacheInitializer : IHostedService
    {
        private string? _marketApiKey;
        private IMemoryCache _memoryCache;

        public ApiCacheInitializer(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _marketApiKey = configuration.GetValue<string>("ApiKeyStrings:MarketApiKey");
            _memoryCache = memoryCache;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _memoryCache.Set("test", "hi");

            //using (HttpClient client = new HttpClient())
            //{
            //    try
            //    {
            //        HttpResponseMessage response = await client.GetAsync($"https://finnhub.io/api/v1/quote?symbol=AAPL&token={_marketApiKey}", cancellationToken);

            //        if (response.IsSuccessStatusCode)
            //        {
            //            string content = await response.Content.ReadAsStringAsync();
            //            Console.WriteLine(content);
            //        }
            //        else
            //        {
            //            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            //        }
            //    }
            //    catch (HttpRequestException ex)
            //    {
            //        Console.WriteLine($"Request error: {ex.Message}");
            //    }
            //}
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
