﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerShop.Data.Migrations;
using BurgerShop.Data.Models;

namespace BurgerShop.Data
{
    public class BurgerShopContext : DbContext
    {
        public BurgerShopContext() : base("BurgerShop")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BurgerShopContext, Configuration>());
        }    

        //public DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        //public DbSet<aspnet_Users> aspnet_Users { get; set; }
        //public DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }       
    }
}