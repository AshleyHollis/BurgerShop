using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using BurgerShop.Core.Models;
using BurgerShop.Data.Models;

namespace BurgerShop.Data
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;

        public GenericRepository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.AsEnumerable();
        }

        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var query = _dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public virtual T Add(T entity)
        {
            return _dbset.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return _dbset.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
    }

    public abstract class GenericRepository<TEntity, TDto> : IGenericRepository<TEntity, TDto>
        where TEntity : BaseEntity 
        where TDto : BaseDto
    {
        protected DbContext _entities;
        protected readonly IDbSet<TEntity> _dbset;

        public GenericRepository(DbContext context)
        {
            _entities = context;
            _dbset = context.Set<TEntity>();
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            var query = _dbset.AsEnumerable();
            return MapToDto<TEntity, TDto>(query);
        }

        public IEnumerable<TDto> FindBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            var query = _dbset.Where(predicate).AsEnumerable();
            return MapToDto<TEntity, TDto>(query);
        }

        public virtual TDto Add(TDto dto)
        {
            var entity = MapToEntity<TDto, TEntity>(dto);
            _dbset.Add(entity);
            
            return MapToDto<TEntity, TDto>(entity);
        }

        public virtual TDto Delete(TDto dto)
        {
            var entity = MapToEntity<TDto, TEntity>(dto);
            _dbset.Remove(entity);

            return MapToDto<TEntity, TDto>(entity);
        }

        public virtual void Edit(TDto dto)
        {
            var entity = MapToEntity<TDto, TEntity>(dto);
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }

        public virtual TDto MapToDto<TEntity, TDto>(TEntity entity)
            where TEntity : class
            where TDto : class
        {
            return Mapper.Map<TEntity, TDto>(entity);
        }

        public virtual IEnumerable<TDto> MapToDto<TEntity, TDto>(IEnumerable<TEntity> entity)
            where TEntity : class
            where TDto : class
        {
            return Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(entity);
        }

        public virtual TEntity MapToEntity<TDto, TEntity>(TDto dto)
            where TDto : class
            where TEntity : class
        {
            return Mapper.Map<TDto, TEntity>(dto);
        }

        public virtual IEnumerable<TEntity> MapToEntityList<TDto, TEntity>(IEnumerable<TDto> dto)
            where TDto : class
            where TEntity : class
        {
            return Mapper.Map<IEnumerable<TDto>, IEnumerable<TEntity>>(dto);
        }
    }
}