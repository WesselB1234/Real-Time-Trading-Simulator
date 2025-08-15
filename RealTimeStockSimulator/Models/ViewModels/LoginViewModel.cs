namespace RealTimeStockSimulator.Models.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginViewModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public LoginViewModel()
        {

        }
    }
}
