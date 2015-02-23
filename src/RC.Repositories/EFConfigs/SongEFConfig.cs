using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RC.Domain;

namespace RC.Repositories.EFConfigs
{
    class SongEFConfig : EntityTypeConfiguration<Song>
    {
        public SongEFConfig()
        {
            HasRequired(s => s.Album);

            HasRequired(s => s.Artist);
        }
    }
}
