using System;
using System.Collections.Generic;
using RC.Core.Interfaces;

namespace RC.Domain
{
    public class Song : IEntity<string>
    {
        public Song()
        {
            Id = Guid.NewGuid().ToString();
        }

        private ICollection<Artist> _featuredArtists; 

        public string Id { get; set; }
        public string Title { get; set; }
        public string AlbumId { get; set; }
        public Album Album { get; set; }
        public string ArtistId { get; set; }
        public Artist Artist { get; set; }
        public TimeSpan Duration { get; set; }

        public ICollection<Artist> FeaturedArtists
        {
            get { return _featuredArtists ?? (_featuredArtists = new List<Artist>()); }
            protected set { _featuredArtists = value; }
        }
    }
}
