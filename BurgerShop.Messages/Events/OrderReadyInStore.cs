namespace BurgerShop.Messages.Events
{
    public class OrderReadyInStore
    {
        public int Id { get; set; }
        public int StoreNo { get; set; }
    }
}