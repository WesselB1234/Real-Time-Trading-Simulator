using RealTimeStockSimulator.Models.Enums;

namespace RealTimeStockSimulator.Models
{
    public class MarketTransactionTradable
    {
        public Tradable Tradable { get; set; }
        public decimal Price { get; set; }
        public MarketTransactionStatus Status { get; set; }
        public int Amount {  get; set; }

        public MarketTransactionTradable(Tradable tradable, decimal price, MarketTransactionStatus status, int amount)
        {
            Tradable = tradable;
            Price = price;
            Status = status;
            Amount = amount;
        }
    }
}
