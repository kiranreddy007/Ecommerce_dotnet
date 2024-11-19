using EcommerceBackend.Models;

namespace EcommerceBackend.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByUsername(string username);

        User GetUserByEmail(string email); // New method to get user by email
        User GetUserById(int id);
        void RegisterUser(User user);
        void UpdateUser(User user); // New method for user updates

        void UpdateUserRole(int id, string role); // New method for updating user role
    }
}