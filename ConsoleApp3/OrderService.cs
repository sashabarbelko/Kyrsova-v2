using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public ServiceResult BuyProduct(int userId, int productId)
        {
            var user = _userRepository.GetUserById(userId);
            var product = _productRepository.GetProductById(productId);

            if (user == null)
            {
                return new ServiceResult { IsSuccess = false, ErrorMessage = "User not found." };
            }

            if (product == null || product.Stock <= 0)
            {
                return new ServiceResult { IsSuccess = false, ErrorMessage = "Product is not available." };
            }

            if (user.Balance < product.Price)
            {
                return new ServiceResult { IsSuccess = false, ErrorMessage = "Insufficient balance." };
            }

            user.Balance -= product.Price;
            product.Stock -= 1;
            _userRepository.SaveChanges();
            _productRepository.SaveChanges();

            var order = new Order
            {
                UserId = userId,
                ProductId = productId,
                Date = DateTime.Now,
                Price = product.Price
            };

            _orderRepository.CreateOrder(order);

            return new ServiceResult
            {
                IsSuccess = true,
                NewBalance = user.Balance,
                ProductName = product.ProductName
            };
        }

        public IEnumerable<Order> GetPurchaseHistory(int userId)
        {
            return _orderRepository.GetOrdersByUserId(userId);
        }
    }
}
