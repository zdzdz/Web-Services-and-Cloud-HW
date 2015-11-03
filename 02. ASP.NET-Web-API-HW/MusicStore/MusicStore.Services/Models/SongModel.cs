namespace MusicStore.Services.Models
{
    using System;
    using System.Linq.Expressions;
    using MusicStore.Models;

    public class SongModel
    {
        public static Expression<Func<Song, SongModel>> FromSong
        {
            get
            {
                return song => new SongModel()
                {
                    Id = song.Id,
                    Title = song.Title,
                    Genre = song.Genre,
                    Year = song.Year
                };
            }
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public int Year { get; set; }
    }
}