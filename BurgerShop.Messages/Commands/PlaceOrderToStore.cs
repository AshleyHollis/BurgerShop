using System.Collections.Generic;
using BurgerShop.Core.Models;

namespace BurgerShop.Messages.Commands
{
    public class PlaceOrderToStore
    {
        public int Id { get; set; }
        public int StoreNo { get; set; }

        public virtual List<ProductDTO> Products { get; set; }
    }
}