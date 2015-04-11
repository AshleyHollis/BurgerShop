using BurgerShop.Core.Models;
using BurgerShop.Data.Models;

namespace BurgerShop.Data.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order, OrderDTO>
    {
        OrderDTO GetById(int id);
    }
}