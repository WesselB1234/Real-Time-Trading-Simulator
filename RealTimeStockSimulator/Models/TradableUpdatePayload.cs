namespace RealTimeStockSimulator.Models
{
    public class TradableUpdatePayload
    {
        public string Symbol { get; set; }
        public TradablePriceInfos TradablePriceInfos { get; set; }

        public TradableUpdatePayload(string symbol, TradablePriceInfos tradablePriceInfos)
        {
            Symbol = symbol;
            TradablePriceInfos = tradablePriceInfos;
        }
    }
}
