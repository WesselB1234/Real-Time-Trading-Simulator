using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Repositories.Interfaces;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Services
{
    public class MarketTransactionsService : IMarketTransactionsService
    {
        private IMarketTransactionsRepository _marketTransactionsRepository;
        public MarketTransactionsService(IMarketTransactionsRepository marketTransactionsRepository)
        {
            _marketTransactionsRepository = marketTransactionsRepository;
        }

        public void AddTransaction(User user, MarketTransactionTradable transaction)
        {
            _marketTransactionsRepository.AddTransaction(user ,transaction);
        }

        public MarketTransactions GetTransactionsByUser(User user)
        {
            return _marketTransactionsRepository.GetTransactionsByUser(user);
        }
    }
}
