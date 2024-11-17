using EcommerceBackend.Models;

namespace EcommerceBackend.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByUsername(string username);
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user); // New method to update user details
    }
}