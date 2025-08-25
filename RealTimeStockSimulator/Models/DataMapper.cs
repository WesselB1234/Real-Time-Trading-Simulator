using Microsoft.Data.SqlClient;
using RealTimeStockSimulator.Models.Enums;
using RealTimeStockSimulator.Models.Interfaces;
using System.Data;

namespace RealTimeStockSimulator.Models
{
    public class DataMapper : IDataMapper
    {
        public User MapUser(SqlDataReader reader)
        {
            int userId = (int)reader["user_id"];
            string userName = (string)reader["username"];
            string email = (string)reader["email"];
            string password = (string)reader["password"];
            decimal money = (decimal)reader["money"];

            return new User(userId, userName, email, password, money);
        }

        public Tradable MapTradable(SqlDataReader reader)
        {
            string symbol = (string)reader["symbol"];

            return new Tradable(symbol);
        }

        public OwnershipTradable MapOwnershipTradable(SqlDataReader reader)
        {
            string symbol = (string)reader["symbol"];
            int amount = (int)reader["amount"];

            return new OwnershipTradable(symbol, amount);
        }

        public MarketTransactionTradable MapMarketTransactionTradable(SqlDataReader reader)
        {
            int transactionId = (int)reader["transaction_id"];
            Tradable tradable = MapTradable(reader);
            decimal price = (decimal)reader["price"];
            MarketTransactionStatus status = (MarketTransactionStatus)Enum.Parse(typeof(MarketTransactionStatus), (string)reader["status"]);
            int amount = (int)reader["amount"];
            DateTime date = (DateTime)reader["date"];

            return new MarketTransactionTradable(transactionId, tradable, price, status, amount, date);
        }

        public OwnershipTradable MapOwnershipTradableByTradable(Tradable tradable, int amount)
        {
            return new OwnershipTradable(tradable.Symbol, amount);
        }
    }
}
