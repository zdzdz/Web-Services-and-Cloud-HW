namespace FeedZillaHttpClient
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Newtonsoft.Json;

    public class Startup
    {
        public static void Main()
        {
            Console.Write("Please enter the search query: ");
            string qureyString = Console.ReadLine();
            Console.Write("Please enter the number of results: ");
            int count = int.Parse(Console.ReadLine());
            Console.WriteLine();

            string uriFeedzilla = "http://api.feedzilla.com/v1/categories/26/articles/search.json?q=" + qureyString;
            string uriJSph = "http://jsonplaceholder.typicode.com/photos";

            //SearchForArticlesHttpClient(qureyString, count, uriJSph);
            SearchForArticlesHttpWebRequest(qureyString, count, uriJSph);
        }

        private static void SearchForArticlesHttpClient(string queryString, int count, string uri)
        {
            Console.WriteLine("Search result: ");

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(uri);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.GetAsync("").Result;

                if (response.IsSuccessStatusCode)
                {
                    var articles = response.Content.ReadAsStringAsync().Result;
                    var articlesCollection = JsonConvert.DeserializeObject<List<Article>>(articles);
                    
                    var result = articlesCollection
                        .Where(a => a.Title.Contains(queryString))
                        .Take(count)
                        .ToList();
                    ////.ForEach(a => Console.WriteLine("{0} - {1}", a.Title, a.Url));

                    foreach (var item in result)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(item.Title + " - ");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine(item.Url);
                    }
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("{0} {1}", (int)response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        private static void SearchForArticlesHttpWebRequest(string queryString, int count, string uri)
        {
            Console.WriteLine("Search result:");

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                using (var responseStream = request.GetResponse().GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        var fzResult = JsonConvert.DeserializeObject<List<Article>>(reader.ReadToEnd());

                        var result = fzResult
                            .Where(a => a.Title.Contains(queryString))
                            .Take(count)
                            .ToList();
                            ////.ForEach(a => Console.WriteLine("{0} - {1}", a.Title, a.Url));

                    foreach (var item in result)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(item.Title + " - ");
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine(item.Url);
                        }
                        Console.ResetColor();
                    }
                }
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                Console.WriteLine(response.StatusCode);
            }
        }
    }
}
