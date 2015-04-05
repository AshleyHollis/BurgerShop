using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BurgerShop.Data.Models;

namespace BurgerShop.Data
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {}

        public override IEnumerable<Product> GetAll()
        {
            return _entities.Set<Product>().AsEnumerable();
        }

        public Product GetById(int id)
        {
            return _dbset.FirstOrDefault(x => x.Id == id);
        }
    }
}