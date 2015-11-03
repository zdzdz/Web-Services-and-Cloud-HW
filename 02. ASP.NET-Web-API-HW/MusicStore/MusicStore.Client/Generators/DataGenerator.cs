namespace MusicStore.Client.Generators
{
    using System;
    using System.Collections.Generic;

    using MusicStore.Models;

    public class DataGenerator : IDataGenerator
    {
        private static DataGenerator instance = null;
        private static readonly object LockThis = new object();

        private DataGenerator()
        {
        }

        public static DataGenerator GetInstance
        {
            get
            {
                lock (LockThis)
                {
                    return instance ?? (instance = new DataGenerator());
                }
            }
        }

        public List<Album> GenerateAlbums(IRandomGenerator generator, int count)
        {
            var albums = new List<Album>();

            for (var i = 0; i < count; i++)
            {
                var album = new Album()
                {
                    Title = generator.GetRandomString(generator.GetRandomNumber(5, 20)),
                    Year = generator.GetRandomNumber(DateTime.Now.AddYears(-50).Year, DateTime.Now.Year),
                    Producer = generator.GetRandomString(generator.GetRandomNumber(5, 20)),
                    Artists = new List<Artist>(),
                    Songs = new List<Song>()
                };
                albums.Add(album);
            }

            return albums;
        }

        public List<Artist> GenerateArtists(IRandomGenerator generator, int count)
        {
            var artists = new List<Artist>();

            for (var i = 0; i < count; i++)
            {
                var artist = new Artist()
                {
                    Name = generator.GetRandomString(generator.GetRandomNumber(5, 20)),
                    DateOfBirth = generator.GetRandomDateTime(DateTime.Now.AddYears(-90), DateTime.Now.AddYears(-20)).ToString(),
                    Country = generator.GetRandomString(generator.GetRandomNumber(5, 20)),
                    Albums = new List<Album>()
                    {
                        new Album()
                        {
                            Title = generator.GetRandomString(generator.GetRandomNumber(5, 20)),
                            Year = generator.GetRandomNumber(DateTime.Now.AddYears(-50).Year, DateTime.Now.Year),
                            Producer = generator.GetRandomString(generator.GetRandomNumber(5, 20)),
                            Artists = new List<Artist>(),
                            Songs = new List<Song>()
                        }
                    },
                    Songs = new List<Song>()
                    {
                        new Song()
                        {
                            Title = generator.GetRandomString(generator.GetRandomNumber(5, 20)),
                            Genre = generator.GetRandomString(generator.GetRandomNumber(5, 20)),
                            Year = generator.GetRandomNumber(DateTime.Now.AddYears(-50).Year, DateTime.Now.Year),
                            ArtistId = null,
                            AlbumId = null
                        }
                    }
                };


                artists.Add(artist);
            }
            return artists;
        }

        public List<Song> GenerateSongs(IRandomGenerator generator, int count)
        {
            var songs = new List<Song>();

            for (var i = 0; i < count; i++)
            {
                var song = new Song()
                {
                    Title = generator.GetRandomString(generator.GetRandomNumber(5, 20)),
                    Genre = generator.GetRandomString(generator.GetRandomNumber(5, 20)),
                    Year = generator.GetRandomNumber(DateTime.Now.AddYears(-50).Year, DateTime.Now.Year),
                    ArtistId = null,
                    AlbumId = null
                };
                songs.Add(song);
            }
            return songs;
        }
    }
}