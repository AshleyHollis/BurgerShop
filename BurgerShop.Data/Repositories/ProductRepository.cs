using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BurgerShop.Core.Models;
using BurgerShop.Data.Models;

namespace BurgerShop.Data
{
    public class ProductRepository : GenericRepository<Product, ProductDTO>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {}

        public ProductDTO GetById(int id)
        {
            throw new NotImplementedException();
            //return _dbset.FirstOrDefault(x => x.Id == id);
        }
    }
}