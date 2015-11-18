using System;
using System.Threading;
using Blacksmith.Core;
using Blacksmith.Core.Responses;

namespace Receiver
{
    public class Startup
    {
        private static string token = "u7Re9s4qvCBzFr4ZZh5u";
        private static string projectId = "564b13f373d0cd00060000ea";

        public static void Main()
        {
            Client client = new Client(projectId, token);

            Console.WriteLine("Listening for new messages from IronMQ server:");

            while (true)
            {
                client.Queue<string>()
                    .Next()
                    .ReleaseImmediatelyOnError()
                    .Consume((message, ctx) =>
                    Console.WriteLine("{{{0} : {1}}} - {2}", message.Id, message.Target, DateTime.Now.ToShortTimeString()));

                Thread.Sleep(100);
            }
        }
    }
}
