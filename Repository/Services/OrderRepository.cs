using ContextDB;
using Microsoft.EntityFrameworkCore;
using tfg.Repository.Base.RepositoryBase;
using tfg.Repository.Services.IOrderRepository;

namespace tfg.Repository.OrderRepository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository 
    { 
        public OrderRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

        public IEnumerable<Order> GetAllOrders()
        { 
            return FindAll().ToList(); 
        }

        public Order GetOrderById(int orderId)
        {
            return FindByCondition(o => o.Id.Equals(orderId))
                .FirstOrDefault();
        }

        public void CreateOrder(Order order)
        {
            Create(order);
        }

        public void DeleteOrder(Order order)
        {
            Delete(order);
        }
    }
}
