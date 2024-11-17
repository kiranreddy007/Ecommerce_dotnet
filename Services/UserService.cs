using EcommerceBackend.Models;
using EcommerceBackend.Repositories;
using EcommerceBackend.Utils;

namespace EcommerceBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public void RegisterUser(User user)
        {
            user.Password = PasswordHasher.HashPassword(user.Password);
            _userRepository.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            var existingUser = _userRepository.GetUserById(user.Id);
            if (existingUser != null)
            {
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Role = user.Role; // Allow role updates if needed
                _userRepository.UpdateUser(existingUser);
            }
        }
    }
}