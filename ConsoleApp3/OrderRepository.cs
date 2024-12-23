using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DBContext _dbContext;

        public OrderRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateOrder(Order order)
        {
            order.OrderID = _dbContext.GenerateOrderID();
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            return _dbContext.Orders.Where(o => o.UserId == userId).ToList();
        }
    }
}
