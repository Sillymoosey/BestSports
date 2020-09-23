
using System.Linq;


namespace OutdoorProducts.Models
{
    public interface IOrderRepository
    {
        IQueryable<Order> Order { get; }
        void SaveOrder(Order order);
    }
}
