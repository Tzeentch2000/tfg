namespace tfg.Repository.Services.IOrderRepository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
        void CreateOrder(Order model);
        //void UpdateOrder(State model);
        void DeleteOrder(Order model);
    }
}