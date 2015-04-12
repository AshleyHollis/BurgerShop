using System.Collections.Generic;
using BurgerShop.Core;

namespace BurgerShop.Data.Models
{
    public class Order : Entity<int>
    {
        public int StoreNo { get; set; }
        public OrderDeliveryMethod OrderDeliveryMethod { get; set; }
        public virtual List<Product> Products { get; set; }      
    }
}