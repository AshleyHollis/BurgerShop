﻿using System.Collections.Generic;
using BurgerShop.Core.Models;
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

    public interface IEntityService<TEntity, TDto> : IService 
        where TEntity : BaseEntity
        where TDto : BaseDto
    {
        void Create(TDto dto);
        void Delete(TDto dto);
        IEnumerable<TDto> GetAll();
        void Update(TDto dto);
    }
}