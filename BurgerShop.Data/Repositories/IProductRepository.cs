using BurgerShop.Core.Models;
using BurgerShop.Data.Models;

namespace BurgerShop.Data
{
    public interface IProductRepository : IGenericRepository<Product, ProductDTO>
    {
        ProductDTO GetById(int id);
    }
}