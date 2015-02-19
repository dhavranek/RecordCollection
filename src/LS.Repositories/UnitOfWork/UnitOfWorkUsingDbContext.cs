using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RC.Core.Interfaces;

namespace RC.Repositories.UnitOfWork
{
    public abstract class UnitOfWorkUsingDbContext<TEntity, TDbContext> : IUnitOfWork<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
        protected readonly DbContext Context;
        protected TDbContext TypedDbContext;

        protected UnitOfWorkUsingDbContext(TDbContext typedContext)
        {
            TypedDbContext = typedContext;
        }

        public virtual DbSet<TEntity> MainEntity
        {
            get { return GetMainDbSetFrom(); }
        }

        protected abstract DbSet<TEntity> GetMainDbSetFrom();

        public DbContext GetContext()
        {
            return Context;
        }

        public TDbContext GetTypedContext()
        {
            return TypedDbContext;
        }

        public void Save()
        {
            TypedDbContext.SaveChanges();
        }

        public void RollBack()
        {
            foreach (var entry in TypedDbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    TypedDbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
