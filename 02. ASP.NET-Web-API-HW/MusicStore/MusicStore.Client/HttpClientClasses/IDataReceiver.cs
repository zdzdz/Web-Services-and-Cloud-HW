namespace MusicStore.Client.HttpClientClasses
{
    using System.Net.Http;

    using Models;

    public interface IDataReceiver
    {
        UnitCollectionsModel GetData(HttpClient client);
    }
}