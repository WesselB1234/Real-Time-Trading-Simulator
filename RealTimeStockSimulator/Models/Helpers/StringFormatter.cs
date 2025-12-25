using RealTimeStockSimulator.Models.Interfaces;
using System.Globalization;

namespace RealTimeStockSimulator.Models.Static
{
    public class StringFormatter : IStringFormatter
    {
        public string FormatDecimalPrice(decimal price)
        {
            return price.ToString("#,##0.00", new CultureInfo("en-US"));
        }

        public string FormatDecimalToJsDecimal(decimal price)
        {
            return price.ToString(CultureInfo.InvariantCulture);
        }
    }
}
