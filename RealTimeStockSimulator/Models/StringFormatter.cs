using RealTimeStockSimulator.Models.Interfaces;
using System.Globalization;

namespace RealTimeStockSimulator.Models
{
    public class StringFormatter : IStringFormatter
    {
        public string FormatDecimalPrice(decimal price)
        {
            return price.ToString("#,##0.00", new CultureInfo("en-US"));
        }
    }
}
