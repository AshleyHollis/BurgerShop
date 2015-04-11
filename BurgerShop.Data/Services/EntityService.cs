using System;
using System.Collections.Generic;
using BurgerShop.Core.Models;
using BurgerShop.Data.Models;
using BurgerShop.Data.Repositories;
using BurgerShop.Data.UnitOfWork;

namespace BurgerShop.Data.Services
{
    public abstract class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> _repository;
        protected EntityService()
        { }

        protected EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _repository.Add(entity);
            _unitOfWork.Commit();
        }


        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Edit(entity);
            _unitOfWork.Commit();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
    }

    public abstract class EntityService<TEntity, TDto> : IEntityService<TEntity, TDto>
        where TEntity : BaseEntity
        where TDto : BaseDto
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity, TDto> _repository;
        protected EntityService()
        { }

        protected EntityService(IUnitOfWork unitOfWork, IGenericRepository<TEntity, TDto> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        // TODO: Implement another method that only returns the Id of the newly created record.
        public virtual TDto Create(TDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException("entity");
            }
            var createdDto = _repository.Add(dto);
            _unitOfWork.Commit();

            return createdDto;
        }


        public virtual void Update(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException("entity");
            _repository.Edit(dto);
            _unitOfWork.Commit();
        }

        public virtual void Delete(TDto dto)
        {
            if (dto == null) throw new ArgumentNullException("entity");
            _repository.Delete(dto);
            _unitOfWork.Commit();
        }

        public virtual IEnumerable<TDto> GetAll()
        {
            return _repository.GetAll();
        }
    }
}