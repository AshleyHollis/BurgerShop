using System.Collections.Generic;
using BurgerShop.Data.Models;

namespace BurgerShop.Data.Repositories
{
    public interface IRepository<T> where T : IEntity<T>
    {
        IEnumerable<T> List { get; }
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindById(int Id);
    }
}