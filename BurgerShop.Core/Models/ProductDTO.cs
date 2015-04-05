namespace BurgerShop.Core.Models
{
    public class ProductDTO : Dto<int>
    {        
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}