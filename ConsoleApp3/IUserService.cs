using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public interface IUserService
    {
        ServiceResult RegisterUser(string username, string password);
        ServiceResult Login(string username, string password);
        ServiceResult AddBalance(int userId, decimal amount);
        IEnumerable<User> GetAllUsers();
    }
}
