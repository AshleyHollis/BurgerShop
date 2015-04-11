using System.Collections.Generic;
using BurgerShop.Core.Models;

namespace BurgerShop.Messages.Events
{
    public class CreateOrder
    {
        public int Id { get; set; }
        public int StoreNo { get; set; }

        public virtual List<ProductDTO> Products { get; set; }
    }
}