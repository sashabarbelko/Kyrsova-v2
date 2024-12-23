using ConsoleApp3;

class Program
{
    static void Main(string[] args)
    {
        DBContext dbContext = new DBContext();
        IUserRepository userRepo = new UserRepository(dbContext);
        IProductRepository productRepo = new ProductRepository(dbContext);
        IOrderRepository orderRepo = new OrderRepository(dbContext);

        IUserService userService = new UserService(userRepo);
        IProductService productService = new ProductService(productRepo);
        IOrderService orderService = new OrderService(orderRepo, productRepo, userRepo);

        UI ui = new UI(userService, productService, orderService);
        ui.ShowMenu();
    }
}
