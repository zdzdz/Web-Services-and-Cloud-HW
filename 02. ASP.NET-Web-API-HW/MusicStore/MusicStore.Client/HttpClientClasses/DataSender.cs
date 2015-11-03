namespace MusicStore.Client.HttpClientClasses
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Models;
    using Common.Constants;
    using MusicStore.Models;

    public class DataSender : IDataSender
    {
        private static DataSender instance = null;
        private static readonly object LockThis = new object();

        private DataSender()
        {
        }

        public static DataSender GetInstance
        {
            get
            {
                lock (LockThis)
                {
                    return instance ?? (instance = new DataSender());
                }
            }
        }

        public bool SentData(UnitCollectionsModel data, HttpClient client)
        {
            try
            {
                var successfullySendSongs = SendSongs(data.Songs, client);
                var successfullySendArtists = SendArtists(data.Artists, client);
                var successfullySendAlbums = SendAlbums(data.Albums, client);

                var successfullySendAllData = successfullySendSongs && successfullySendArtists && successfullySendAlbums;
                return successfullySendAllData;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool SendSongs(IEnumerable<Song> songs, HttpClient client)
        {
            foreach (
                StringContent postContent in songs.Select(song => new StringContent(JsonConvert.SerializeObject(song))))
            {
                postContent.Headers.ContentType = new MediaTypeHeaderValue(WebServiceConstants.MediaTypeHeaderJson);

                var response = client.PostAsync(WebServiceConstants.PostSongsRoute, postContent).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool SendArtists(IEnumerable<Artist> artists, HttpClient client)
        {
            foreach (var postContent in artists.Select(artist => new StringContent(JsonConvert.SerializeObject(artist)))
                )
            {
                postContent.Headers.ContentType = new MediaTypeHeaderValue(WebServiceConstants.MediaTypeHeaderJson);

                var response = client.PostAsync(WebServiceConstants.PostArtistsRoute, postContent).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool SendAlbums(IEnumerable<Album> albums, HttpClient client)
        {
            foreach (
                StringContent postContent in
                    albums.Select(album => new StringContent(JsonConvert.SerializeObject(album))))
            {
                postContent.Headers.ContentType = new MediaTypeHeaderValue(WebServiceConstants.MediaTypeHeaderJson);

                var response = client.PostAsync(WebServiceConstants.PostAlbumsRoute, postContent).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }

            return true;
        }
    }
}