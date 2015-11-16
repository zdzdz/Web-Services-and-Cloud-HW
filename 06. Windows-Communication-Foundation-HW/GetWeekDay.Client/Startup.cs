using System;
using GetWeekDay.Client.ServiceReferenceWeekDay;

namespace GetWeekDay.Client
{
    public class Startup
    {
        public static void Main()
        {
            ServiceWeekDayClient weekDayService = new ServiceWeekDayClient();
            Console.WriteLine(weekDayService.GetData(DateTime.Now));
        }
    }
}
