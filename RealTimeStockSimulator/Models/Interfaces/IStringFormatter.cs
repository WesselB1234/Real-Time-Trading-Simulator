namespace RealTimeStockSimulator.Models.Interfaces
{
    public interface IStringFormatter
    {
        string FormatDecimalPrice(decimal price);
        string FormatDecimalToJsDecimal(decimal value);
    }
}
