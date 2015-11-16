using System.ServiceModel.Description;

namespace StringCompareHost
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using StringCompare;

    public class Startup
    {
        public static void Main()
        {
            Console.WriteLine("Run as administrator (bin -> debug -> StringCompareHost.exe)");

            Uri serviceAddress = new Uri("http://localhost:8000/string");
            ServiceHost selfHost = new ServiceHost(typeof(StringCompare), serviceAddress);

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            selfHost.Description.Behaviors.Add(smb);

            using (selfHost)
            {
                selfHost.Open();
                Console.WriteLine("The service is started at endpoint {0}", serviceAddress);

                Console.WriteLine("Press [Enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
