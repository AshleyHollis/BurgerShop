using BurgerShop.Data.Models;

namespace BurgerShop.Data
{
    public interface IProductService : IEntityService<Product>
    {
        Product GetById(int Id);
    }
}