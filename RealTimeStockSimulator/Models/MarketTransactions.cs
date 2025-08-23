namespace RealTimeStockSimulator.Models
{
    public class MarketTransactions
    {
        public User User { get; set; }
        public List<MarketTransactionTradable> Transactions { get; set; }

        public MarketTransactions(User user, List<MarketTransactionTradable> transactions)
        {
            User = user;
            Transactions = transactions;
        }
    }
}
