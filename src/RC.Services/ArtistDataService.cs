using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RC.Domain;
using RC.Repositories;
using RC.Repositories.DBContexts;
using RC.Repositories.UnitOfWork;

namespace RC.Services
{
    public class ArtistDataService : BaseDataService<Artist, string, ApplicationDbContext>
    {
        
        public override BaseRepository<Artist, string, ApplicationDbContext> GetDefaultRepository()
        {
            return new ArtistRepository();
        }

        public override UnitOfWorkUsingDbContext<Artist, ApplicationDbContext> GetDefaultUnitOfWork()
        {
            return new ArtistUnitOfWork();
        }
    }
}
