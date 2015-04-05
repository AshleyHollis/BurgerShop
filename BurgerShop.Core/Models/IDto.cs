namespace BurgerShop.Core.Models
{
    public interface IDto<T>
    {
        T Id { get; set; }
    }
}