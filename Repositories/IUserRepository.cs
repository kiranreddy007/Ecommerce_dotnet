using EcommerceBackend.Models;

namespace EcommerceBackend.Repositories
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        void AddUser(User user);
    }
}