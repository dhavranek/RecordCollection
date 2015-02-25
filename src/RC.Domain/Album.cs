using System;
using System.Collections.Generic;
using RC.Core.Interfaces;

namespace RC.Domain
{
    public class Album : IEntity<string>
    {
        public Album()
        {
            Id = Guid.NewGuid().ToString();
        }

        private ICollection<Song> _songs;
        private ICollection<MediaFormat> _availableFormats;

        public string Id { get; set; }
        public string Title { get; set; }
        public string ArtistId { get; set; }
        public Artist Artist { get; set; }
        public DateTime ReleaseYear { get; set; }

        public virtual ICollection<Song> Songs
        {
            get { return _songs ?? (_songs = new List<Song>()); }
            protected set { _songs = value; }
        }

        public MediaFormat PreferredFormat { get; private set; }

        public virtual ICollection<MediaFormat> AvailableFormats
        {
            get { return _availableFormats ?? (_availableFormats = new List<MediaFormat>()); }
            protected set { _availableFormats = value; }
        }
    }

    public enum MediaFormat
    {
        Vinyl = 0,
        Cassette = 1,
        CD = 2,
        MP3 = 3,
        InternetRadio = 4
    }
}
