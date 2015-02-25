using System.Data.Entity.ModelConfiguration;
using RC.Domain;

namespace RC.Repositories.EFConfigs
{
    class AlbumEFConfig : EntityTypeConfiguration<Album>
    {
        public AlbumEFConfig()
        {
            HasRequired(a => a.Artist)
                .WithMany(a => a.Albums)
                .HasForeignKey(a => a.ArtistId)
                .WillCascadeOnDelete(false);
        }
    }
}
