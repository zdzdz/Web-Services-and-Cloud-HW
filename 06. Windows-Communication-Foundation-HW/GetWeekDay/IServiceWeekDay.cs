namespace GetWeekDay
{
    using System;
    using System.ServiceModel;

    [ServiceContract]
    public interface IServiceWeekDay
    {

        [OperationContract]
        string GetData(DateTime dateTime);
    }
}
