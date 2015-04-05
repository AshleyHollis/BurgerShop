using BurgerShop.Data.Models;

namespace BurgerShop.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BurgerShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BurgerShop.Data.BurgerShopContext";
        }

        protected override void Seed(BurgerShopContext context)
        {
            context.Products.AddOrUpdate(p => p.ID,
                new Product {ID = 1, Name = "Product01", ProductCode = "P001", Description = "First Product"},
                new Product {ID = 2, Name = "Product02", ProductCode = "P002", Description = "Second Product"},
                new Product {ID = 3, Name = "Product03", ProductCode = "P003", Description = "Third Product"}
                );

            context.SaveChanges();
        }
    }
}