using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Repositories.Interfaces;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Services.HostedServices
{
    public class TestingCacheInitializer : IHostedService
    {
        private ITradablePriceInfosService _priceInfosService;
        private ITradablesService _tradablesService;
        private Random _random = new Random();

        public TestingCacheInitializer(ITradablePriceInfosService priceInfosService, ITradablesService tradablesService)
        {
            _priceInfosService = priceInfosService;
            _tradablesService = tradablesService;
        } 

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (Tradable tradable in _tradablesService.GetAllTradables())
            {
                _priceInfosService.SetPriceInfosBySymbol(tradable.Symbol, new TradablePriceInfos(_random.Next(1,10000)));
            }

            await Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
