using BookProject.Data.Repositories.IRepository;
using BookProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookProject.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookDbContext _context;

        public UserRepository(BookDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsersInMemory()
        {
            var users = _context.Users.ToList(); // Load data into memory
            return users.Where(u => u.Username.Contains("admin")).ToList(); // Filter in memory
        }

        public List<User> GetAllUsersFromDatabase()
        {
            var usersQuery = _context.Users.Where(u => u.Username.Contains("admin")); // Create query
            return usersQuery.ToList(); // Execute query on the database
        }

        public User GetUserById(int id)
        {
            return _context.Users.SingleOrDefault(u => u.UserId == id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<User> GetUsersOrderedByUsername()
        {
            return _context.Users.OrderBy(u => u.Username).ToList();
        }

        public List<IGrouping<string, User>> GetUsersGroupedByRole()
        {
            return _context.Users.GroupBy(u => u.Role.RoleName).ToList();
        }

        public List<dynamic> GetUsersWithRoles() // Adjusted return type to List<dynamic>
        {
            var usersWithRoles = from user in _context.Users
                                 join role in _context.Roles
                                 on user.RoleId equals role.RoleId
                                 select new
                                 {
                                     Username = user.Username,
                                     RoleName = role.RoleName
                                 };
            return usersWithRoles.ToList<dynamic>(); // Adjusted to return List<dynamic>
        }
    }
}
