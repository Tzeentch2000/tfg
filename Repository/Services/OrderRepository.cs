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

        public IEnumerable<Order> getAllOrdersWithDetails()
        {
            return FindAll()
               .Include(o => o.Book).ThenInclude(b => b.Categories)
               .Include(o => o.Book).ThenInclude(b => b.State)
               .ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return FindByCondition(o => o.Id.Equals(orderId))
                .FirstOrDefault();
        }

        public Order getOrderByIdWithDetails(int orderId)
        {
            return FindByCondition(o => o.Id.Equals(orderId))
                .Include(o => o.Book).ThenInclude(b => b.Categories)
                .Include(o => o.Book).ThenInclude(b => b.State)
                .FirstOrDefault();
        }
         public IEnumerable<Order> GetOrderByUserId(int userId)
        {
             return FindByCondition(o => o.UserId.Equals(userId))
                .Include(o => o.Book).ToList();
        }

        public void createOrders(IEnumerable<Order> orders){
            RepositoryContext.Set<Order>().AttachRange(orders);
        }

        public void CreateOrder(Order order)
        {
            Create(order);
        }

        public Order CreateOrderWithDetails(Order model)
        {
             RepositoryContext.Set<Order>().Attach(model);
             return model;
        }

        public void DeleteOrder(Order order)
        {
            Delete(order);
        }

        public IEnumerable<Order> getOrdersByDateAscending(int id)
        {
            return FindByCondition(o => o.UserId.Equals(id))
            .Include(o => o.Book).OrderBy(o => o.Date).ToList();
        }

        public IEnumerable<Order> getOrdersByDateDescending(int id)
        {
            return FindByCondition(o => o.UserId.Equals(id))
            .Include(o => o.Book).OrderByDescending(o => o.Date).ToList();
        }

        public IEnumerable<Order> getOrdersByCategories(int id)
        {
            return FindAll()
               .Include(o => o.Book).ThenInclude(b => b.Categories)
               .Include(o => o.Book).ThenInclude(b => b.State)
               .Where(o => o.Book.Categories.Any(c => c.Id == id))
               .ToList();
        }

        public IEnumerable<Order> getOrdersByPriceAscending(int id)
        {
             return FindByCondition(o => o.UserId.Equals(id))
            .Include(o => o.Book).OrderBy(o => (o.Amount*o.Book.Price)).ToList();
        }

        public IEnumerable<Order> getOrdersByPriceDescending(int id)
        {
             return FindByCondition(o => o.UserId.Equals(id))
            .Include(o => o.Book).OrderByDescending(o => (o.Amount*o.Book.Price)).ToList();
        }
    }
}
