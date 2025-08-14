using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        User? GetUserByName(string userName);
        User AddUser(User user);
        User? GetUserByLoginCredentials(string userName, string password);
    }
}
