using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByUsername(string username);
        User GetUserById(int id);
        void RegisterUser(User user);
        void UpdateUser(User user); // New method for user updates
    }
}