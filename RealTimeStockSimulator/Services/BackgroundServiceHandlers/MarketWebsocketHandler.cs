using Microsoft.AspNetCore.SignalR;
using RealTimeStockSimulator.Hubs;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Repositories.Interfaces;
using RealTimeStockSimulator.Services.Interfaces;
using System.Text.Json;

namespace RealTimeStockSimulator.Services.BackgroundServiceHandlers
{
    public class MarketWebsocketHandler : IMarketWebsocketHandler
    {
        private IHubContext<MarketHub> _hubContext;
        private ITradablePriceInfosService _priceInfosService;

        public MarketWebsocketHandler(IHubContext<MarketHub> hubContext, ITradablePriceInfosService priceInfosService)
        {
            _hubContext = hubContext;
            _priceInfosService = priceInfosService;
        }

        public async Task HandleMarketWebSocketPayload(IncomingMarketWebsocketPayload marketPayload)
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
    }
}
