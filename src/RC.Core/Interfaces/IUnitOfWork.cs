using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC.Core.Interfaces
{
    //TODO: Figure out how to allow DbContext constraint on TDbContext without exposing Repository to projects outside of Service projects.
    public interface IUnitOfWork<TEntity, TDbContext> : IDisposable where TEntity : class where TDbContext : DbContext
    {
        DbSet<TEntity> MainEntity { get; }
        DbContext GetContext();
        TDbContext GetTypedContext();
        void Save();
        void RollBack();
    }
}
