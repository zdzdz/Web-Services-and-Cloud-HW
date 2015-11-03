namespace MusicStore.Client.Generators
{
    using System;

    public interface IRandomGenerator
    {
        int GetRandomNumber(int min, int max);

        string GetRandomString(int length);

        DateTime GetRandomDateTime(DateTime from, DateTime to);
    }
}