using Microsoft.Data.SqlClient;
using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Repositories.Interfaces;

namespace RealTimeStockSimulator.Repositories
{
    public class DbUsersRepository : DbBaseRepository, IUsersRepository
    {
        public DbUsersRepository(IConfiguration configuration) : base(configuration){}

        private User ReadUser(SqlDataReader reader)
        {
            int userId = (int)reader["user_id"];
            string userName = (string)reader["user_name"];
            string email = (string)reader["email"];
            string password = (string)reader["password"];
            decimal money = (decimal)reader["money"];

            return new User(userId, userName, email, password, money);
        }

        public User AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users(username, email, password, money) " +
                    $"VALUES (@Username, @Email, @Password, @Money);" +
                    "SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Money", user.Money);

                command.Connection.Open();
                user.UserId = Convert.ToInt32(command.ExecuteScalar());

                return user;
            }
        }

        public User? GetUserByName(string userName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE username = @UserName";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UserName", userName);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    return ReadUser(reader);
                }
            }

            return null;
        }

        public User? GetUserByLoginCredentials(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Users WHERE username = @UserName AND password = @Password";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    return ReadUser(reader);
                }
            }

            return null;
        }
    }
}
