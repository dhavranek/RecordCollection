using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.InteropServices;
using RC.Core;
using RC.Core.Interfaces;
using RC.Repositories;
using RC.Repositories.UnitOfWork;

namespace RC.Services
{
    public abstract class BaseDataService<TEntity, TPK, TDbContext> : IDataService<TEntity, TPK>
        where TEntity : class, IEntity<TPK>
        where TDbContext : DbContext
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

        protected BaseDataService()
        {
            BaseRepository = GetDefaultRepository();
            UnitOfWork = GetDefaultUnitOfWork();
        }

        public abstract BaseRepository<TEntity, TPK, TDbContext> GetDefaultRepository();

        public abstract UnitOfWorkUsingDbContext<TEntity, TDbContext> GetDefaultUnitOfWork();

        public BaseRepository<TEntity, TPK, TDbContext> GetRepository()
        {
            return BaseRepository ?? GetDefaultRepository();
        }

        public UnitOfWorkUsingDbContext<TEntity, TDbContext> GetUnitOfWork()
        {
            return UnitOfWork ?? GetDefaultUnitOfWork();
        }

        public ServiceProcessingResult<List<TEntity>> GetAll()
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var result = new ServiceProcessingResult<List<TEntity>>();

                try
                {
                    var getAllResult = GetRepository().GetAll(unitOfWork, GetDefaultIncludes());
                    result.IsSuccessful = true;
                    result.Data = getAllResult.Data;
                }
                catch (Exception ex)
                {
                    // TODO: Log Failure
                    result.IsSuccessful = false;
                }

                return result;
            }
        }

        public ServiceProcessingResult<TEntity> Get(TPK id)
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var result = new ServiceProcessingResult<TEntity>();
                try
                {
                    var getResult = GetRepository().Get(id, unitOfWork);
                    result.IsSuccessful = true;
                    result.Data = getResult.Data;
                }
                catch (Exception)
                {
                    // TODO: Log Failure
                    result.IsSuccessful = false;
                }
                return result;
            }
        }

        public ServiceProcessingResult<TEntity> Add(TEntity entityToAdd)
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var result = new ServiceProcessingResult<TEntity>();
                try
                {
                    var addResult = GetRepository().Add(entityToAdd, unitOfWork);
                    result.IsSuccessful = true;
                    result.Data = addResult.Data;
                }
                catch (Exception)
                {
                    // TODO: Log Failure
                    result.IsSuccessful = false;
                }
                return result;
            }
        } 

        public ServiceProcessingResult<TEntity> Upsert(TEntity entity)
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var result = new ServiceProcessingResult<TEntity>();
                try
                {
                    var upsertResult = GetRepository().Update(entity, unitOfWork);
                    result.IsSuccessful = true;
                    result.Data = upsertResult.Data;
                }
                catch (Exception)
                {
                    // TODO: Log Failure
                    result.IsSuccessful = false;
                }
                return result;            
            }
        }

        public ServiceProcessingResult Delete(TPK id)
        {
            using (var unitOfWork = GetUnitOfWork())
            {
                var result = new ServiceProcessingResult();
                try
                {
                    var deleteResult = GetRepository().Delete(id, unitOfWork);
                    if (!deleteResult.IsSuccessful)
                    {
                        result.IsSuccessful = false;
                        return result;
                    }
                    result.IsSuccessful = true;
                }
                catch (Exception)
                {
                    // TODO: Log Failure
                    result.IsSuccessful = false;
                }
                return result;
            }
        }





        protected virtual string[] GetDefaultIncludes()
        {
            return new string[] { };
        }
    }
}
