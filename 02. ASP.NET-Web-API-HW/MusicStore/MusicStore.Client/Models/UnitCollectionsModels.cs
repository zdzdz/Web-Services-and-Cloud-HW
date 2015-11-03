namespace MusicStore.Client.Models
{
    using System.Collections.Generic;

    using MusicStore.Models;

    public class UnitCollectionsModel
    {
        public List<Song> Songs { get; set; }

        public List<Artist> Artists { get; set; }

        public List<Album> Albums { get; set; }
    }
}