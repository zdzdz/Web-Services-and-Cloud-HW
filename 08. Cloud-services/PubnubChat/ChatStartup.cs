using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PubnubChat
{
    using System.Diagnostics;
    using System.Threading;

    public class ChatStartup
    {
        private const string PUBLISH_KEY = "pub-c-8f0d4abb-5128-4562-934b-838f657c61d1";
        private const string SUBSCRIBE_KEY = "sub-c-9e3bccbe-8d58-11e5-bf00-02ee2ddab7fe";
        private const string SECRET_KEY = "sec-c-Y2QzNTEwNmUtYzNlNS00NDgyLWI1MWMtNzBiZWJlNWIzN2E1";
        private static int x;
        private static int y;
        private static string msg;

        public static void Main()
        {
            Process.Start(Path.GetFullPath("../../PubNubClient.html"));

            Thread.Sleep(2000);

            PubnubAPI pubnub = new PubnubAPI(PUBLISH_KEY, SUBSCRIBE_KEY, SECRET_KEY);
            string channel = "chat-channel";

            List<object> publishResult = pubnub.Publish(channel, "Hi");
            Console.WriteLine("Publish success: " + publishResult[0] + "\n" +
                "Publish Info: " + publishResult[1]);

            object serverTime = pubnub.Time();
            Console.WriteLine("Server time: " + DateTime.Parse(serverTime.ToString()));

            Task t = new Task(() => pubnub.Subscribe(
                channel,
                delegate (object message)
                {
                    Console.WriteLine("Received message => " + "\"" + message + "\"");
                    Console.WriteLine(new string('=', 60));
                    Console.WriteLine();
                    return true;
                }));
            t.Start();

            if (t.Status == TaskStatus.Running)
            {
                Console.WriteLine("hi");
            }

            while (true)
            {
                Console.WriteLine("Enter a message: ");
                msg = Console.ReadLine();
                pubnub.Publish(channel, msg);
                Console.WriteLine("Message \"{0}\" sent.\n", msg);
                
                Thread.Sleep(1000);
            }
        }
    }
}
