using AutoMapper;
using BurgerShop.Core.Models;
using BurgerShop.Data.Models;
using BurgerShop.Messages.Commands;
using BurgerShop.Messages.Events;

namespace BurgerShop.Data
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Product, ProductDTO>();
            Mapper.CreateMap<Product, ProductDTO>().ReverseMap();
            Mapper.CreateMap<Order, OrderDTO>();
            Mapper.CreateMap<Order, OrderDTO>().ReverseMap();
            Mapper.CreateMap<OrderDTO, CreateOrder>();
            Mapper.CreateMap<OrderDTO, CreateOrder>().ReverseMap();
            Mapper.CreateMap<CreateOrder, PlaceOrderToStore>();
            Mapper.CreateMap<CreateOrder, PlaceOrderToStore>().ReverseMap();
            Mapper.CreateMap<PlaceOrderToStore, OrderBeingMade>();
            Mapper.CreateMap<PlaceOrderToStore, OrderBeingMade>().ReverseMap();
        }
    }
}