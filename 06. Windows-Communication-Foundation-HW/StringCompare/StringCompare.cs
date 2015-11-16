namespace StringCompare
{
    using System.Text.RegularExpressions;

    public class StringCompare : IStringCompare
    {
        public int Contains(string firstString, string secondString)
        {
            var count = Regex.Matches(secondString, firstString).Count;
            return count;
        }
    }
}
