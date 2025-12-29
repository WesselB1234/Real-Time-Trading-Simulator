using System.Text.Json.Serialization;

namespace RealTimeStockSimulator.Models
{
    public class OwnershipTradable : Tradable
    {
        [JsonInclude]
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

        public OwnershipTradable(string symbol, string? name, string? imagePath, int amount) : base(symbol, name, imagePath)
        {
            Amount = amount;
        }
    }
}
