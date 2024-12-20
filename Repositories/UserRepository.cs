using System.Linq;
using EcommerceBackend.Models;
using EcommerceBackend.Data;

namespace EcommerceBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(User user)
        {
            if (_context != null)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
        }

        public void UpdateUser(User user)
        {
            if (_context != null)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }
    }
}