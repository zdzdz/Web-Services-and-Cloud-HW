namespace MusicStore.Data
{
    using Models;
    using Repositories;

    public interface IMusicStoreData
    {
        IRepository<Artist> Artists { get; }

        IRepository<Album> Albums { get; }

        IRepository<Song> Songs { get; }

        void SaveChanges();
    }
}