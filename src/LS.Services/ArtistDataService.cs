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
        public ArtistDataService(BaseRepository<Artist, string, ApplicationDbContext> repository, UnitOfWorkUsingDbContext<Artist, ApplicationDbContext> unitOfWork) : base(repository, unitOfWork)
        {
        }

        public override BaseRepository<Artist, string, ApplicationDbContext> GetDefaultRepository()
        {
            throw new NotImplementedException();
        }

        public override UnitOfWorkUsingDbContext<Artist, ApplicationDbContext> GetDefaultUnitOfWork()
        {
            throw new NotImplementedException();
        }
    }
}
