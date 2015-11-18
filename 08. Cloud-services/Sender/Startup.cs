namespace Sender
{
    using System;
    using Blacksmith.Core;

    public class Startup
    {
        private static string token = "u7Re9s4qvCBzFr4ZZh5u";
        private static string projectId = "564b13f373d0cd00060000ea";

        public static void Main()
        {
            Client client = new Client(projectId, token);
            var queue = client.Queue<string>();

            while (true)
            {
                Console.WriteLine("Please enter the message you want to send ot type exit to close the application: ");
                string msg = Console.ReadLine();

                if (msg.ToLower() == "exit")
                {
                    break;
                }

                queue.Push(msg);

                Console.WriteLine("Your message has been send to the IronMQ server.\n");
            }
        }
    }
}
