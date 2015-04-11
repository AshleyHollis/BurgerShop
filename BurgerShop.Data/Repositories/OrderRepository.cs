using System;
using System.Data.Entity;
using BurgerShop.Core.Models;
using BurgerShop.Data.Models;

namespace BurgerShop.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order, OrderDTO>, IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context)
        {}

        public OrderDTO GetById(int id)
        {
            throw new NotImplementedException();
            //return _dbset.FirstOrDefault(x => x.Id == id);
        }
    }
}