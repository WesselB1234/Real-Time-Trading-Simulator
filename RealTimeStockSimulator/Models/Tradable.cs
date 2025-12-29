namespace RealTimeStockSimulator.Models
{
    public class Tradable
    {
        public string Symbol { get; set; }
        public string? Name { get; set; }
        public byte[]? Image { get; set; }
        public TradablePriceInfos? TradablePriceInfos { get; set; }

        public Tradable(string symbol) 
        { 
            Symbol = symbol;
        }

        public Tradable(string symbol, string? name, byte[]? image)
        {
            Symbol = symbol;
            Name = name;
            Image = image;
        }
    }
}
