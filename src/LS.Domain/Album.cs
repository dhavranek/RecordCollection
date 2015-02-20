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

        private ICollection<MediaFormat> _availableFormats;

        public string Id { get; set; }
        public string Title { get; set; }
        public Artist Artist { get; set; }
        public DateTime ReleaseYear { get; set; }
        public MediaFormat PreferredFormat { get; private set; }
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
