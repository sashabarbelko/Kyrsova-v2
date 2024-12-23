using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public interface IOrderService
    {
        ServiceResult BuyProduct(int userId, int productId);
        IEnumerable<Order> GetPurchaseHistory(int userId);
    }
}
