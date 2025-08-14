using RealTimeStockSimulator.Models;
using RealTimeStockSimulator.Repositories.Interfaces;
using RealTimeStockSimulator.Services.Interfaces;

namespace RealTimeStockSimulator.Services
{
    public class UsersService : IUsersService
    {
        IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public User AddUser(User user)
        {
            return _usersRepository.AddUser(user);
        }

        public User? GetUserByLoginCredentials(string userName, string password)
        {
            return _usersRepository.GetUserByLoginCredentials(userName, password);
        }

        public User? GetUserByName(string userName)
        {
            return _usersRepository.GetUserByName(userName);
        }
    }
}
