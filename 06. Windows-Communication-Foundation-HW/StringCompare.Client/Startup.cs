namespace StringCompare.Client
{
    using System;

    public class Startup
    {
        public static void Main()
        {
            string searchTerm = "hi";
            string text = "Hello hi, hi, hellomy name is Mihil.";

            var client = new StringCompareClient();

            var result = client.Contains(searchTerm, text);

            Console.WriteLine("The text: \"{0}\" \nContains word \"{1}\" - {2} times", text, searchTerm, result);
        }
    }
}
