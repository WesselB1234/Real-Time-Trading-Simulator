using Microsoft.Data.SqlClient;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Models.Interfaces;
using RealTimeStockSimulator.Repositories.Interfaces;

namespace RealTimeStockSimulator.Repositories
{
    public class DbOwnershipRepository : DbBaseRepository, IOwnershipsRepository
    {
        public DbOwnershipRepository(IConfiguration configuration, IDataMapper dataMapper) : base(configuration, dataMapper) { }

        public Ownership GetOwnershipByUser(User user)
        {
            Ownership ownership = new Ownership(user, new List<OwnershipTradable>());
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT symbol, amount " +
                    "FROM Ownership " +
                    "WHERE user_id = @UserId;";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UserId", ownership.User.UserId);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ownership.Tradables.Add(_dataMapper.MapOwnershipTradable(reader));
                }
            }

            return ownership;
        }
    }
}
