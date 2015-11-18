namespace DropboxPhotosPublisher
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;

    using DropNet;

    public class Startup
    {
        private const string apiKey = "==Your key here==";
        private const string apiSecret = "Your app secret here";
        private static string date = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

        public static void Main()
        {
            var client = new DropNetClient(apiKey, apiSecret);

            client.GetToken();
            var url = client.BuildAuthorizeUrl();

            Console.WriteLine("Open \"{0}\" in browser.", url);
            Console.WriteLine("Press [Enter] when you click on [Allow]");
            OpenBrowser(url);

            Console.ReadLine();
            client.GetAccessToken();

            client.UseSandbox = true;
            var metaData = client.CreateFolder("HW-Pictures - " + date);

            string[] dir = Directory.GetFiles("../../images/", "*.jpg");
            foreach (var file in dir)
            {
                Console.Write("Uploading");
                while (true)
                {
                    FileStream stream = File.Open(file, FileMode.Open);
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);

                    client.UploadFile("/" + metaData.Name, file.Substring(6), bytes);

                    for (var i = 0; i < 10; i++)
                    {
                        Console.Write(".");
                        Thread.Sleep(300);
                    }

                    stream.Close();

                    Console.WriteLine();
                    break;
                }
            }

            Console.WriteLine("Done!");
            var pictureUrl = client.GetShare(metaData.Path);
            OpenBrowser(pictureUrl.Url);
        }

        private static void OpenBrowser(string url)
        {
            if (File.Exists(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe"))
            {
                Process.Start(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe", url);
            }
            else if (File.Exists(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"))
            {
                Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe", url);
            }
            else if (File.Exists(@"C:\Program Files (x86)\Internet Explorer\iexplore.exe"))
            {
                Process.Start(@"C:\Program Files (x86)\Internet Explorer\iexplore.exe", url);
            }
        }
    }
}
