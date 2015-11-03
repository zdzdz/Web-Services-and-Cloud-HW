namespace MusicStore.Client.HttpClientClasses
{
    using System.Net.Http;

    using Models;

    public interface IDataSender
    {
        bool SentData(UnitCollectionsModel data, HttpClient client);
    }
}