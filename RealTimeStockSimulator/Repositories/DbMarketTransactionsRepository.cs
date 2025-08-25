using Microsoft.Data.SqlClient;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Models.Interfaces;
using RealTimeStockSimulator.Repositories.Interfaces;

namespace RealTimeStockSimulator.Repositories
{
    public class DbMarketTransactionsRepository : DbBaseRepository, IMarketTransactionsRepository
    {
        public DbMarketTransactionsRepository(IConfiguration configuration, IDataMapper dataMapper) : base(configuration, dataMapper) { }

        public void AddTransaction(User user, MarketTransactionTradable transaction)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Transactions(user_id, symbol, price, status, amount) " +
                    $"VALUES (@UserId, @Symbol, @Price, @Status, @Amount);";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UserId", user.UserId);
                command.Parameters.AddWithValue("@Symbol", transaction.Tradable.Symbol);
                command.Parameters.AddWithValue("@Price", transaction.Price);
                command.Parameters.AddWithValue("@Status", transaction.Status);
                command.Parameters.AddWithValue("@Amount", transaction.Amount);

                command.Connection.Open();
                command.ExecuteScalar();
            }
        }

        public MarketTransactions GetTransactionsByUser(User user)
        {
            MarketTransactions transactions = new MarketTransactions(user, new List<MarketTransactionTradable>());

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT symbol, price, status, amount " +
                   "FROM Transactions " +
                   "WHERE user_id = @UserId;";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UserId", user.UserId);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                     transactions.Transactions.Add(_dataMapper.MapMarketTransactionTradable(reader));
                }
            }

            return transactions;
        }
    }
}
