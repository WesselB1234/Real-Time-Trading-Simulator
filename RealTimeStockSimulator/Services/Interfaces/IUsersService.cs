using RealTimeStockSimulator.Models;

namespace RealTimeStockSimulator.Services.Interfaces
{
    public interface IUsersService
    {
        User? GetUserByName(string userName);
        int AddUser(User user);
        User? GetUserByLoginCredentials(string userName, string password);
        void UpdateUser(User user);
    }
}
