namespace BurgerShop.Data.Models
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}