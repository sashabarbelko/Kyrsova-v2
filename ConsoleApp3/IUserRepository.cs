using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        User GetUserByUsername(string username);
        User GetUserById(int userId);
        IEnumerable<User> GetAllUsers();
        void SaveChanges();
    }
}
