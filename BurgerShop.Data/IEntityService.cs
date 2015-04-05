using System.Collections.Generic;
using BurgerShop.Data.Models;

namespace BurgerShop.Data
{

    public interface IEntityService<T> : IService where T : BaseEntity
    {
        void Create(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}