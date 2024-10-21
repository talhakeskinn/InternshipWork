using StopajHesapAPI.Models;
using StopajHesapAPI.Services.Abstraction;


namespace StopajHesapAPI.Services.Concerete
{
    public class UserService : IUserService
    {
        readonly List<UserModel> _users;
        public UserService()
        {
            _users = new List<UserModel>()
            {
                new UserModel{userName = "user1", password = "12345" }

            };
        }
        public Task<UserModel> Login(string username, string password)
        {
            var user = _users.FirstOrDefault(x => x.userName == username && x.password == password);
            return Task.FromResult(user); //Task.FromResult metodu, bulunan kullanıcıyı Task<UserModel> türünde döndürür.
            int[] deneme = new int[4] { 0, 1, 2, 3 };
        }
    }
}


