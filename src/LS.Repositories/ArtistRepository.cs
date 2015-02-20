using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RC.Core;
using RC.Domain;
using RC.Repositories.DBContexts;
using RC.Repositories.UnitOfWork;

namespace RC.Repositories
{
    public class ArtistRepository : BaseRepository<Artist, string, ApplicationDbContext>
    {
    }
}
