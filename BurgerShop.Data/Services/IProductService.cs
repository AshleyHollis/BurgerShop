using BurgerShop.Core.Models;
using BurgerShop.Data.Models;

namespace BurgerShop.Data
{
    public interface IProductService : IEntityService<Product, ProductDTO>
    {
        ProductDTO GetById(int Id);
    }
}