using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using RC.Domain;


namespace RC.Repositories.DBContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
    }
}
