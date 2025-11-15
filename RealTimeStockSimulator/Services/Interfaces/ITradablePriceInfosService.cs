using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface ITradablePriceInfosService
    {
        TradablePriceInfos? GetPriceInfosBySymbol(string symbol);
        List<string> GetAllKeys();
        void SetPriceInfosBySymbol(string symbol, TradablePriceInfos priceInfos);
    }
}
