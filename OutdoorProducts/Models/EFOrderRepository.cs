using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OutdoorProducts.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private StoreDBContext context;
        public EFOrderRepository(StoreDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Order => context.Order
        .Include(o => o.Lines)
       .ThenInclude(l => l.Product);
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                context.Order.Add(order);
            }
            context.SaveChanges();
        }
    }
}