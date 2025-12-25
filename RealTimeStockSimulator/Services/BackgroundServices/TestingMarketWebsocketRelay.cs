
using Microsoft.AspNetCore.SignalR;
using RealTimeStockSimulator.Hubs;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Repositories.Interfaces;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace RealTimeStockSimulator.Services.BackgroundServices
{
    public class TestingMarketWebsocketRelay : BackgroundService
    {   
        private IHubContext<MarketHub> _hubContext;
        private ITradablePriceInfosService _priceInfosService;

        public TestingMarketWebsocketRelay(IHubContext<MarketHub> hubContext, ITradablePriceInfosService priceInfosService)
        {
            _hubContext = hubContext;
            _priceInfosService = priceInfosService;
        }

        private async Task HandleMarketWebSocketPayload(IncomingMarketWebsocketPayload marketPayload)
        {
            IncomingMarketWebsocketTradable responseTradable = marketPayload.Data[marketPayload.Data.Count - 1];
            TradablePriceInfos? tradablePriceInfos = _priceInfosService.GetPriceInfosBySymbol(responseTradable.Symbol);

            if (responseTradable.Price != null && tradablePriceInfos != null && tradablePriceInfos.Price != responseTradable.Price)
            {
                tradablePriceInfos.Price = (decimal)responseTradable.Price;
                TradableUpdatePayload tradableUpdatePayload = new TradableUpdatePayload(responseTradable.Symbol, tradablePriceInfos);

                _priceInfosService.SetPriceInfosBySymbol(responseTradable.Symbol, tradablePriceInfos);

                await _hubContext.Clients.All.SendAsync("ReceiveMarketData", JsonSerializer.Serialize(tradableUpdatePayload), CancellationToken.None);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            

            await Task.CompletedTask;
        }
    }
}
