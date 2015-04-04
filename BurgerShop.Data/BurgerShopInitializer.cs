using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerShop.Data.Models;

namespace BurgerShop.Data
{
    public class BurgerShopInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BurgerShopContext>
    {
        protected override void Seed(BurgerShopContext context)
        {
            var products = new List<Product>
            {
                new Product {ID = 1, Name = "Product01", ProductCode = "P001", Description = "First Product"},
                new Product {ID = 2, Name = "Product02", ProductCode = "P002", Description = "Second Product"},
                new Product {ID = 3, Name = "Product03", ProductCode = "P003", Description = "Third Product"}

            };

            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();
        }
    }
}
