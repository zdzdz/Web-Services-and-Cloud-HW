namespace GetWeekDay
{
    using System;
    using System.Globalization;
    using System.Threading;

    public class ServiceWeekDay : IServiceWeekDay
    {
        public string GetData(DateTime dateTime)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("bg-BG");
            return string.Format("Current Day of a Week in bulgarian: {0}", 
                dateTime.ToString("dddd", new CultureInfo("bg-BG")));
        }
    }
}
