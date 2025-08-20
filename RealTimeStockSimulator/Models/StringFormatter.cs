using RealTimeStockSimulator.Models.Interfaces;

namespace RealTimeStockSimulator.Models
{
    public class StringFormatter : IStringFormatter
    {
        public string FormatDecimalPrice(decimal price)
        {
            return price.ToString("#,##0.00");
        }
    }
}
