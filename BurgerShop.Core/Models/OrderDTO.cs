using System;
using System.Collections.Generic;

namespace BurgerShop.Core.Models
{
    public class OrderDTO : Dto<int>
    {
        public int StoreNo { get; set; }

        public virtual List<ProductDTO> Products { get; set; }
    }
}