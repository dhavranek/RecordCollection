using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using RC.Core.Interfaces;

namespace RC.Domain
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        public ApplicationUser() : base()
        {
            IsDeleted = false;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
         
        public bool IsDeleted { get; set; }
    }

}
