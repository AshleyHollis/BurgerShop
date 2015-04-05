namespace BurgerShop.Data.Models
{
    public class Product : Entity<int>
    {        
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}