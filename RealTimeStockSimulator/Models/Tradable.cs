using RealTimeStockSimulator.Models.Enums;

namespace RealTimeStockSimulator.Models
{
    public class Tradable
    {
        public string Symbol { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public TradableType Type { get; set; }
        public TradablePriceInfos? TradablePriceInfos { get; set; }

        public Tradable(string symbol, string? name, string? imagePath, TradableType type)
        {
            Symbol = symbol;
            Name = name;
            ImagePath = imagePath;
            Type = type;
        }
    }
}
