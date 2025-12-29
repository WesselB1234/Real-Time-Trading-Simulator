namespace RealTimeStockSimulator.Models
{
    public class Tradable
    {
        public string Symbol { get; set; }
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public TradablePriceInfos? TradablePriceInfos { get; set; }

        public Tradable(string symbol) 
        { 
            Symbol = symbol;
        }

        public Tradable(string symbol, string? name, string? imagePath)
        {
            Symbol = symbol;
            Name = name;
            ImagePath = imagePath;
        }
    }
}
