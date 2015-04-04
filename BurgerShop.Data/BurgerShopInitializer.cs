using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerShop.Data.Models;

namespace BurgerShop.Data
{
    internal class BurgerShopInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BurgerShopContext>
    {
        protected override void Seed(BurgerShopContext context)
        {
            var products = new List<Product>
            {
                new Product {ID = 1, Name = "Product01", ProductCode = "P001", Description = "First Product"}

            };

            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();
        }
    }
}
