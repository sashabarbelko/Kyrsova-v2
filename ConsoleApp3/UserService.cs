using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ServiceResult RegisterUser(string username, string password)
        {
            var existingUser = _userRepository.GetUserByUsername(username);
            if (existingUser != null)
            {
                return new ServiceResult { IsSuccess = false, ErrorMessage = "User already exists." };
            }

            User newUser = new User { Username = username, Password = password };
            _userRepository.CreateUser(newUser);
            return new ServiceResult { IsSuccess = true };
        }

        public ServiceResult Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null || user.Password != password)
            {
                return new ServiceResult { IsSuccess = false, ErrorMessage = "Invalid username or password." };
            }
            return new ServiceResult { IsSuccess = true, User = user };
        }

        public ServiceResult AddBalance(int userId, decimal amount)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return new ServiceResult { IsSuccess = false, ErrorMessage = "User not found." };
            }

            user.Balance += amount;
            _userRepository.SaveChanges();
            return new ServiceResult { IsSuccess = true, NewBalance = user.Balance };
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
    }
}
