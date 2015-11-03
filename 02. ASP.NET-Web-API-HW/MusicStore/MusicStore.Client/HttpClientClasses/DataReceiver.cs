namespace MusicStore.Client.HttpClientClasses
{
    using System.Net.Http;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Models;
    using Common.Constants;
    using MusicStore.Models;

    public class DataReceiver : IDataReceiver
    {
        private static DataReceiver instance = null;
        private static readonly object LockThis = new object();

        private DataReceiver()
        {
        }

        public static DataReceiver GetInstance
        {
            get
            {
                lock (LockThis)
                {
                    return instance ?? (instance = new DataReceiver());
                }
            }
        }

        public UnitCollectionsModel GetData(HttpClient client)
        {
            var unitCollectionsModel = new UnitCollectionsModel()
            {
                Songs = GetSongs(client),
                Artists = GetArtists(client),
                Albums = GetAlbums(client)
            };

            return unitCollectionsModel;
        }

        private static List<Song> GetSongs(HttpClient httpClient)
        {
            var response = httpClient.GetAsync(WebServiceConstants.GetSongsRoute).Result;
            var responseAsJson = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<Song>>(responseAsJson);

            return result;
        }

        private static List<Artist> GetArtists(HttpClient httpClient)
        {
            var response = httpClient.GetAsync(WebServiceConstants.GetArtistsRoute).Result;
            var responseAsJson = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<Artist>>(responseAsJson);

            return result;
        }

        private static List<Album> GetAlbums(HttpClient httpClient)
        {
            var response = httpClient.GetAsync(WebServiceConstants.GetAlbumsRoute).Result;
            var responseAsJson = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<List<Album>>(responseAsJson);

            return result;
        }
    }
}