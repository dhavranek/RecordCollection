using System;
using System.Collections.Generic;
using System.Data.Entity;
using RC.Core;
using RC.Core.Interfaces;
using RC.Repositories;
using RC.Repositories.UnitOfWork;

namespace RC.Services
{
    public abstract class BaseDataService<TEntity, TPK, TDbContext> : IDataService<TEntity, TPK> where TEntity : class, IEntity<TPK> where TDbContext : DbContext

    {
        // Add Logger

        protected readonly UnitOfWorkUsingDbContext<TEntity, TDbContext> UnitOfWork;

        protected BaseRepository<TEntity, TPK, TDbContext> BaseRepository { get; set; }

        protected BaseDataService(BaseRepository<TEntity, TPK, TDbContext> repository,
            UnitOfWorkUsingDbContext<TEntity, TDbContext> unitOfWork)
        {
            BaseRepository = repository;
            UnitOfWork = unitOfWork;
        }

        public ServiceProcessingResult<List<TEntity>> GetAll()
        {
            throw new NotImplementedException();
        }

        public ServiceProcessingResult<TEntity> Get(TPK id)
        {
            throw new NotImplementedException();
        }

        public ServiceProcessingResult<TEntity> Upsert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public ServiceProcessingResult Delete(TPK id)
        {
            throw new NotImplementedException();
        }
    }
}
