namespace tfg.Repository.Services.IOrderRepository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> getAllOrdersWithDetails();
        Order GetOrderById(int id);
        Order getOrderByIdWithDetails(int id);

        void createOrders(IEnumerable<Order> orders);
        void CreateOrder(Order model);
        Order CreateOrderWithDetails(Order model);
        //void UpdateOrder(State model);
        void DeleteOrder(Order model);
    }
}