namespace RealTimeStockSimulator.Models
{
    public class OwnershipTradable : Tradable
    {
        public int Amount;
        public decimal TotalValue
        {
            get
            {
                if (TradablePriceInfos != null)
                {
                    return TradablePriceInfos.Price * Amount;
                }

                return 0;
            }
        }

        public OwnershipTradable(string symbol, int amount) : base(symbol)
        {
            Amount = amount;
        }
    }
}
