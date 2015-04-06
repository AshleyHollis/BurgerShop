using BurgerShop.Core.Models;
using BurgerShop.Data.Models;

namespace BurgerShop.Data
{
    public interface IOrderRepository : IGenericRepository<Order, OrderDTO>
    {
        OrderDTO GetById(int id);
    }
}