namespace RealTimeStockSimulator.Models.ViewModels
{
    public class BuySellViewModel
    {
        public string? Symbol { get; set; }
        public int? Amount { get; set; }

        public BuySellViewModel(string symbol, int amount)
        {
            Symbol = symbol;
            Amount = amount;
        }

        public BuySellViewModel()
        {

        }
    }
}
