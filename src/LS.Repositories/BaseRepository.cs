using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using LinqKit;
using RC.Core;
using RC.Core.Interfaces;

namespace RC.Repositories
{
    public abstract class BaseRepository<TEntity, TPK, TDbContext>
        where TEntity : class, IEntity<TPK>
        where TDbContext : DbContext
    {
        //TODO: Consider catching data-access specific exceptions and handling accordingling versus blanket Exception catch
        public virtual DataAccessResult<List<TEntity>> GetAll(IUnitOfWork<TEntity, TDbContext> unitOfWork, params string[] includes)
        {
            var result = new DataAccessResult<List<TEntity>>();

            try
            {
                var allEntities = unitOfWork.MainEntity.IncludeMany(includes).ToList();
                result.IsSuccessful = true;
                result.Data = allEntities;
            }
            catch (Exception ex)
            {
                //TODO: Log failure
                result.IsSuccessful = false;
            }

            return result;
        }

        public virtual DataAccessResult<List<TEntity>> GetAllWhere(Expression<Func<TEntity, bool>> predicate,
            IUnitOfWork<TEntity, TDbContext> unitOfWork, params string[] includes)
        {
            var result = new DataAccessResult<List<TEntity>>();

            try
            {
                var builder = unitOfWork.MainEntity.IncludeMany(includes).AsExpandable().Where(predicate);
                var allEntities = builder.ToList();
                result.IsSuccessful = true;
                result.Data = allEntities;
            }
            catch (Exception)
            {
                //TODO: Log Failure
                result.IsSuccessful = false;
            }

            return result;
        }

        public virtual DataAccessResult<List<TEntity>> GetAllWhere(IList<Expression<Func<TEntity, bool>>> predicates,
            IUnitOfWork<TEntity, TDbContext> unitOfWork, params string[] includes)
        {
            var result = new DataAccessResult<List<TEntity>>();

            try
            {
                var builder = unitOfWork.MainEntity.IncludeMany(includes);
                predicates.ForEach(p => builder = builder.Where(p));

                var allEntities = builder.ToList();

                result.IsSuccessful = true;
                result.Data = allEntities;
            }
            catch (Exception)
            {
                //TODO: Log Failure
                result.IsSuccessful = false;
            }

            return result;
        }

        public virtual DataAccessResult<List<TEntity>> GetAllWhereOrdered(
            Expression<Func<TEntity, bool>> predicate, 
            string orderBy, bool descending, 
            IUnitOfWork<TEntity, TDbContext> unitOfWork,
            params string[] includes)
        {
            var result = new DataAccessResult<List<TEntity>>();

            try
            {
                var builder = unitOfWork.MainEntity.IncludeMany(includes).AsExpandable().Where(predicate);
                builder = @descending ? builder.OrderBy("it." + orderBy + " descending") : builder.OrderBy("it." + orderBy);

                var allOrderedEntities = builder.ToList();

                result.IsSuccessful = true;
                result.Data = allOrderedEntities;

            }
            catch (Exception)
            {
                //TODO: Log Failure
                result.IsSuccessful = false;
            }
            return result;
        }

        public virtual PagedDataAccessResult<List<TEntity>> PagedGetAllWhereOrdered(
            List<Expression<Func<TEntity, bool>>> predicates,
            string orderBy, bool descending,
            int pageSize, int pageNumber,
            IUnitOfWork<TEntity, TDbContext> unitOfWork,
            params string[] includes)
        {
            var result = new PagedDataAccessResult<List<TEntity>>();

            try
            {
                var builder = unitOfWork.MainEntity.IncludeMany(includes);
                predicates.ForEach(p => builder = builder.Where(p));

                var skip = pageSize * pageNumber;
                var take = pageSize;
                var count = builder.Count();

                builder = descending
                    ? builder.OrderBy("it." + orderBy + " descending").Skip(skip).Take(take)
                    : builder.OrderBy("it." + orderBy).Skip(skip).Take(take);

                var pagedEntities = builder.ToList();

                result.IsSuccessful = true;
                result.Data = pagedEntities;
                result.HasNext = count - (skip + take) > 0;
                result.HasPrevious = skip > 0;
                result.Count = count;
                result.CurrentPage = pageNumber;
            }
            catch (Exception)
            {
                //TODO: Log Failure
                result.IsSuccessful = false;
            }

            return result;
        }



        public virtual DataAccessResult<TEntity> Get(TPK byId, IUnitOfWork<TEntity, TDbContext> unitOfWork)
        {
            var result = new DataAccessResult<TEntity>();
            try
            {
                var singleEntity = unitOfWork.MainEntity.Find(byId);

                result.Data = singleEntity;
                result.IsSuccessful = true;
            }
            catch (Exception)
            {
                //TODO: Log Failure
                result.IsSuccessful = false;
            }
            return result;
        }

        public virtual DataAccessResult<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate,
            IUnitOfWork<TEntity, TDbContext> unitOfWork, params string[] includes)
        {
            var result = new DataAccessResult<TEntity>();
            try
            {
                var subset = unitOfWork.MainEntity.IncludeMany(includes).AsExpandable().Where(predicate).SingleOrDefault();

                result.Data = subset;
                result.IsSuccessful = true;
            }
            catch (Exception)
            {
                //TODO: Log Failure
                result.IsSuccessful = false;
            }
            return result;
        }

        public virtual DataAccessResult<TEntity> Add(TEntity entityToAdd, IUnitOfWork<TEntity, TDbContext> unitOfWork)
        {
            var result = new DataAccessResult<TEntity>();
            try
            {
                unitOfWork.MainEntity.Add(entityToAdd);
                result.IsSuccessful = true;
                result.Data = entityToAdd;
            }
            catch (Exception)
            {
                //TODO: Log Failure
                result.IsSuccessful = false;
            }
            return result;
        }

        public virtual DataAccessResult Delete(TPK id, IUnitOfWork<TEntity, TDbContext> unitOfWork)
        {
            var result = new DataAccessResult();
            var entityToDelete = GetEntity(id, unitOfWork);
            unitOfWork.MainEntity.Attach(entityToDelete);
            unitOfWork.MainEntity.Remove(entityToDelete);

            result.IsSuccessful = true;
            return result;
        }

        public virtual DataAccessResult<TEntity> Update(TEntity entityToUpdate,
            IUnitOfWork<TEntity, TDbContext> unitOfWork)
        {
            var result = new DataAccessResult<TEntity>();
            try
            {
                var context = unitOfWork.GetTypedContext();
                var existingEntity = GetEntity(entityToUpdate.Id, unitOfWork);
                context.Entry(existingEntity).CurrentValues.SetValues(entityToUpdate);

                result.IsSuccessful = true;
                result.Data = entityToUpdate;
            }
            catch (Exception)
            {
                //TODO: Log Failure
                result.IsSuccessful = false;
            }
            return result;
        }

        protected virtual TEntity GetEntity(TPK id, IUnitOfWork<TEntity, TDbContext> unitOfWork)
        {
            return unitOfWork.MainEntity.Find(id);
        }
    }
}
