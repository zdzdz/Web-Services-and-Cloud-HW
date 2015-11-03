namespace MusicStore.Services.Models
{
    using System;
    using System.Linq.Expressions;

    using MusicStore.Models;

    public class AlbumModel
    {
        public static Expression<Func<Album, AlbumModel>> FromAlbum
        {
            get
            {
                return album => new AlbumModel()
                {
                    Id = album.Id,
                    Title = album.Title,
                    Year = album.Year,
                    Producer = album.Producer
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string Producer { get; set; }
    }
}