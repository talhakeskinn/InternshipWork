using System.ComponentModel.DataAnnotations;

namespace StopajHesapAPI.Models
{
    public class UserModel
    {
        public const string userOptions = "User";
        public string userName { get; set; } = String.Empty;
        public string password { get; set; } = String.Empty;
    }
}
