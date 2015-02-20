using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RC.Domain;
using RC.Repositories.DBContexts;

namespace RC.Repositories.UnitOfWork
{
    public class ArtistUnitOfWork : UnitOfWorkUsingDbContext<Artist, ApplicationDbContext>
    {
        public ArtistUnitOfWork(ApplicationDbContext typedContext) : base(typedContext)
        {
        }

        protected override DbSet<Artist> GetMainDbSetFrom()
        {
            return TypedDbContext.Artists;
        }
    }
}
