using System.Data.Entity.ModelConfiguration;
using RC.Domain;

namespace RC.Repositories.EFConfigs
{
    class ArtistEFConfig : EntityTypeConfiguration<Artist>
    {
        public ArtistEFConfig()
        {
            HasMany(a => a.Albums)
                .WithRequired();

            HasMany(a => a.Songs)
                .WithRequired();
        }
    }
}
