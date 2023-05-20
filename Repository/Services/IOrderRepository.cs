namespace tfg.Repository.Services.IOrderRepository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> getAllOrdersWithDetails();
        Order GetOrderById(int id);
        Order getOrderByIdWithDetails(int id);
        IEnumerable<Order> GetOrderByUserId(int id);
        void createOrders(IEnumerable<Order> orders);
        void CreateOrder(Order model);
        Order CreateOrderWithDetails(Order model);
        //void UpdateOrder(State model);
        void DeleteOrder(Order model);
        IEnumerable<Order> getOrdersByDateAscending(int id);
        IEnumerable<Order> getOrdersByDateDescending(int id);
        IEnumerable<Order> getOrdersByPriceAscending(int id);
        IEnumerable<Order> getOrdersByPriceDescending(int id);
        IEnumerable<Order> getOrdersByCategories(int id);
    }
}