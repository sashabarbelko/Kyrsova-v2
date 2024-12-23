using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class DBContext
    {
        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }

        public DBContext()
        {
            Users = new List<User>
        {
            new User { UserId = 1, Username = "alice", Password = "password123", Balance = 100.0m },
            new User { UserId = 2, Username = "bob", Password = "password456", Balance = 50.0m },
            new User { UserId = 3, Username = "carol", Password = "password789", Balance = 200.0m }
        };

            Products = new List<Product>
        {
            new Product { ProductID = 1, ProductName = "Laptop", Price = 500.0m, Stock = 10 },
            new Product { ProductID = 2, ProductName = "Smartphone", Price = 300.0m, Stock = 5 },
            new Product { ProductID = 3, ProductName = "Headphones", Price = 50.0m, Stock = 20 },
            new Product { ProductID = 4, ProductName = "Keyboard", Price = 100.0m, Stock = 15 }
        };

            Orders = new List<Order>();
        }

        public int GenerateUserID()
        {
            return Users.Count + 1;
        }

        public int GenerateOrderID()
        {
            return Orders.Count + 1;
        }


        public void SaveChanges()
        {
            // В реальній програмі тут зберігатиметься зміни в базі даних
        }
    }
}
