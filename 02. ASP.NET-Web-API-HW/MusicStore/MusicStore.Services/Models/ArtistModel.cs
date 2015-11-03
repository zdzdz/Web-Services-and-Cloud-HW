namespace MusicStore.Services.Models
{
    using System.Linq.Expressions;
    using System;

    using MusicStore.Models;

    public class ArtistModel
    {
        public static Expression<Func<Artist, ArtistModel>> FromArtist
        {
            get
            {
                return artist => new ArtistModel()
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    DateOfBirth = artist.DateOfBirth,
                    Country = artist.Country
                };
            }
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string DateOfBirth { get; set; }

        public string Country { get; set; }
    }
}