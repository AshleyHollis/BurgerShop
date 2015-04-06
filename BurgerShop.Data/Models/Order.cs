using System.Collections.Generic;

namespace BurgerShop.Data.Models
{
    public class Order : Entity<int>
    {
        public int StoreNo { get; set; }
        public virtual List<Product> Products { get; set; }      
    }
}