using StopajHesapAPI.Models;

namespace StopajHesapAPI.Services.Abstraction
{
    public interface IUserService
    {
        public Task<UserModel> Login(string username, string password);  
    }
}
