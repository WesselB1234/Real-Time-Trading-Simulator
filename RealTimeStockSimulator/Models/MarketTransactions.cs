namespace RealTimeStockSimulator.Models
{
    public class MarketTransactions
    {
        public UserAccount User { get; set; }
        public List<MarketTransactionTradable> Transactions { get; set; }

        public MarketTransactions(UserAccount user, List<MarketTransactionTradable> transactions)
        {
            User = user;
            Transactions = transactions;
        }
    }
}
