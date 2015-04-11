using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BurgerShop.Core.Models;
using BurgerShop.Data.Models;

namespace BurgerShop.Data.Repositories
{

    public interface IGenericRepository<T> 
        where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
    }

    public interface IGenericRepository<TEntity, TDto>
        where TEntity : BaseEntity
        where TDto : BaseDto
    {
        IEnumerable<TDto> GetAll();
        IEnumerable<TDto> FindBy(Expression<Func<TEntity, bool>> predicate);
        TDto Add(TDto dto);
        TDto Delete(TDto dto);
        void Edit(TDto dto);
        void Save();
    }
}