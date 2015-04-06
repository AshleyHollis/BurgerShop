using BurgerShop.Core.Models;
using BurgerShop.Data.Models;

namespace BurgerShop.Data
{
    public interface IOrderService : IEntityService<Order, OrderDTO>
    {
        OrderDTO GetById(int Id);
    }
}