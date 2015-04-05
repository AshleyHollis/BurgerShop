using BurgerShop.Data.Models;

namespace BurgerShop.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Product GetById(int id);
    }
}