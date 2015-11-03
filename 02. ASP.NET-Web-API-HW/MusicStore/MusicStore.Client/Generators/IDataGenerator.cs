namespace MusicStore.Client.Generators
{
    using System.Collections.Generic;

    using MusicStore.Models;

    public interface IDataGenerator
    {
        List<Song> GenerateSongs(IRandomGenerator generator, int count);

        List<Artist> GenerateArtists(IRandomGenerator generator, int count);

        List<Album> GenerateAlbums(IRandomGenerator generator, int count);
    }
}