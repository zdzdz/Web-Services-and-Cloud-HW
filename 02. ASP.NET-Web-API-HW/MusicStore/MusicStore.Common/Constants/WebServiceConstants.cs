namespace MusicStore.Common.Constants
{
    public static class WebServiceConstants
    {
        public const string BaseUri = "http://localhost:8295/";

        public const string GetSongsRoute = "api/Song/All";

        public const string GetArtistsRoute = "api/Artist/All";

        public const string GetAlbumsRoute = "api/Album/All";

        public const string PostSongsRoute = "api/Song/Create";

        public const string PostArtistsRoute = "api/Artist/Create";

        public const string PostAlbumsRoute = "api/Album/Create";

        public const string MediaTypeHeaderJson = "application/json";
    }
}
