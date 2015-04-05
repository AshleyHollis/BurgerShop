namespace BurgerShop.Core.Models
{
    public abstract class BaseDto
    {}

    public abstract class Dto<T> : BaseDto, IDto<T>
    {
        public virtual T Id { get; set; }
    }
}