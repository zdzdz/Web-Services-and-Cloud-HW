namespace MusicStore.Client
{
    using System;
    using System.Net.Http;

    using Models;
    using Generators;
    using Common.Constants;
    using HttpClientClasses;

    public static class Client
    {
        public static void Execute()
        {
            // Declare classes
            IRandomGenerator randomGenerator;
            IDataGenerator dataGenerator;
            IDataSender dataSender;
            IDataReceiver dataReceiver;

            // Initialize classes
            InitializeGenerators(out randomGenerator, out dataGenerator);
            InitializeHttpClients(out dataSender, out dataReceiver);

            // Generate data
            var generatedData = GenerateData(randomGenerator, dataGenerator);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(WebServiceConstants.BaseUri);

                // Sent data
                SentDataToService(dataSender, generatedData, client);

                // Get data
                var dataFromService = GetDataFromService(dataReceiver, client);

                // Print data
                PrintData(dataFromService);
            }
        }

        private static void PrintData(UnitCollectionsModel data)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Songs:");
            Console.WriteLine();

            var counter = 0;
            foreach (var song in data.Songs)
            {
                counter += 1;
                if (song.Artist != null && song.Artist.Name != null)
                {
                    Console.WriteLine("{0}. Title: {1}, Year: {2}, Artist name: {3}", counter, song.Title, song.Year,
                        song.Artist.Name);
                }
                else
                {
                    Console.WriteLine("{0}. Title: {1}, Year: {2}, Artist name: ", counter, song.Title, song.Year);
                }
            }
            Console.WriteLine();

            Console.WriteLine("Artists");
            Console.WriteLine();

            counter = 0;
            foreach (var artist in data.Artists)
            {
                counter += 1;
                Console.WriteLine("{0}. Name: {1}, Country: {2}, Date of birth: {3}, Number of songs: {4}, Number of albums: {5}",
                    counter, artist.Name, artist.Country, artist.DateOfBirth, artist.Songs.Count, artist.Albums.Count);
            }
            Console.WriteLine();

            Console.WriteLine("Albums");
            Console.WriteLine();

            counter = 0;
            foreach (var album in data.Albums)
            {
                counter += 1;
                Console.WriteLine("{0}. Title: {1}, Producer: {2}, Year: {3}, Number of artists: {4}, Number of songs: {5}",
                    counter, album.Title, album.Producer, album.Year, album.Artists.Count, album.Songs.Count);
            }
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------");
        }

        private static UnitCollectionsModel GetDataFromService(IDataReceiver dataReceiver, HttpClient client)
        {
            var data = dataReceiver.GetData(client);

            return data;
        }

        private static void SentDataToService(IDataSender dataSender, UnitCollectionsModel generatedData, HttpClient client)
        {
            var response = dataSender.SentData(generatedData, client);
            Console.WriteLine(response ? "Data is successfully sended!" : "Data is unsuccessfully sended!");
        }

        private static void InitializeGenerators(out IRandomGenerator randomGenerator, out IDataGenerator dataGenerator)
        {
            randomGenerator = RandomGenerator.GetInstance;
            dataGenerator = DataGenerator.GetInstance;
        }

        private static void InitializeHttpClients(out IDataSender dataSender, out IDataReceiver dataReceiver)
        {
            dataSender = DataSender.GetInstance;
            dataReceiver = DataReceiver.GetInstance;
        }

        private static UnitCollectionsModel GenerateData(IRandomGenerator generator, IDataGenerator randomGenerator)
        {
            var generatedData = new UnitCollectionsModel()
            {
                Songs = randomGenerator.GenerateSongs(generator, 20),
                Albums = randomGenerator.GenerateAlbums(generator, 20),
                Artists = randomGenerator.GenerateArtists(generator, 20)
            };

            return generatedData;
        }
    }
}
