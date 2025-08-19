using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        User? GetUserByName(string userName);
        int AddUser(User user);
        User? GetUserByLoginCredentials(string userName, string password);
    }
}
