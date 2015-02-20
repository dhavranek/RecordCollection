using System;
using System.Collections.Generic;
using RC.Core.Interfaces;

namespace RC.Domain
{
    public class Artist : IEntity<string>
    {
        public Artist()
        {
            Id = Guid.NewGuid().ToString();
        }

        private ICollection<Album> _albums ;
        private ICollection<Song> _songs; 

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Album> Albums
        {
            get { return _albums ?? (_albums = new List<Album>()); }
            protected set { _albums = value; }
        }

        public virtual ICollection<Song> Songs
        {
            get { return _songs ?? (_songs = new List<Song>()); }
            protected set { _songs = value; }
        }

    }
}
