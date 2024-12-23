using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class UI
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public UI(IUserService userService, IProductService productService, IOrderService orderService)
        {
            _userService = userService;
            _productService = productService;
            _orderService = orderService;
        }

        public void ShowMenu()
        {
            User currentUser = null;

            while (true)
            {
                Console.WriteLine("╔═══════════════════════════════╗");
                Console.WriteLine("║          MAIN MENU            ║");
                Console.WriteLine("╠═══════════════════════════════╣");
                Console.WriteLine("║ 1 - Register User             ║");
                Console.WriteLine("║ 2 - Login                     ║");
                Console.WriteLine("║ 3 - View Products             ║");
                Console.WriteLine("║ 4 - Add Balance               ║");
                Console.WriteLine("║ 5 - Buy Product               ║");
                Console.WriteLine("║ 6 - View Purchase History     ║");
                Console.WriteLine("║ 7 - Show all clients          ║");
                Console.WriteLine("║ 8 - Exit                      ║");
                Console.WriteLine("╚═══════════════════════════════╝");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Try again.");
                    continue;
                }

                if (choice == 8) break;

                switch (choice)
                {
                    case 1:
                        RegisterUser();
                        break;

                    case 2:
                        currentUser = Login();
                        break;

                    case 3:
                        ViewProducts();
                        break;

                    case 4:
                        AddBalance(currentUser);
                        break;

                    case 5:
                        BuyProduct(currentUser);
                        break;

                    case 6:
                        ViewPurchaseHistory(currentUser);
                        break;

                    case 7:
                        ShowAllClients();
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        continue;
                }
            }
        }

        private void RegisterUser()
        {
            Console.WriteLine("Enter Username");
            string username = Console.ReadLine();
            Console.WriteLine("Enter the password");
            string password = Console.ReadLine();

            var result = _userService.RegisterUser(username, password);
            if (result.IsSuccess)
            {
                Console.WriteLine($"User {username} registered successfully.");
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }

        private User Login()
        {
            Console.WriteLine("Enter Username");
            string username = Console.ReadLine();
            Console.WriteLine("Enter Password");
            string password = Console.ReadLine();

            var result = _userService.Login(username, password);
            if (result.IsSuccess)
            {
                Console.WriteLine($"Welcome {username}");
                return result.User;
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
                return null;
            }
        }

        private void ViewProducts()
        {
            var products = _productService.GetAllProducts();
            Console.WriteLine("Available Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, Price: {product.Price}, Stock: {product.Stock}");
            }
        }

        private void AddBalance(User currentUser)
        {
            if (currentUser == null)
            {
                Console.WriteLine("Please log in first.");
                return;
            }

            Console.WriteLine("Enter amount to add to your balance: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            var result = _userService.AddBalance(currentUser.UserId, amount);
            if (result.IsSuccess)
            {
                Console.WriteLine($"Your new balance is {result.NewBalance}");
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }

        private void BuyProduct(User currentUser)
        {
            if (currentUser == null)
            {
                Console.WriteLine("Please log in first.");
                return;
            }

            Console.WriteLine("Enter Product ID to buy:");
            int productId = int.Parse(Console.ReadLine());

            var result = _orderService.BuyProduct(currentUser.UserId, productId);
            if (result.IsSuccess)
            {
                Console.WriteLine($"You have successfully purchased {result.ProductName}. Your new balance is {result.NewBalance}");
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }

        private void ViewPurchaseHistory(User currentUser)
        {
            if (currentUser == null)
            {
                Console.WriteLine("Please log in first.");
                return;
            }

            var orders = _orderService.GetPurchaseHistory(currentUser.UserId);
            Console.WriteLine("Your Purchase History:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.OrderID}, Date: {order.Date}, Total: {order.Price}");
                foreach (var product in order.Products)
                {
                    Console.WriteLine($"  Product: {product.ProductName}, Price: {product.Price}");
                }
            }
        }

        private void ShowAllClients()
        {
            var users = _userService.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"User ID: {user.UserId}, Balance: {user.Balance}, Name: {user.Username}");
            }
        }
    }
}
