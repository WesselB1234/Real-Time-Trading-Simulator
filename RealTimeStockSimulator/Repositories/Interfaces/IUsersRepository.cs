using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        List<User> GetAllUsers();
        User? GetUserByName(string userName);
        int AddUser(User user);
        User? GetUserByLoginCredentials(string userName, string password);
        void UpdateUser(User user);
    }
}
