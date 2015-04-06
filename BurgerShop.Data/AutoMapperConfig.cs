using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BurgerShop.Core.Models;
using BurgerShop.Data.Models;

namespace App.BurgerShop.Web.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Product, ProductDTO>();
            Mapper.CreateMap<Product, ProductDTO>().ReverseMap();
            Mapper.CreateMap<Order, OrderDTO>();
            Mapper.CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}