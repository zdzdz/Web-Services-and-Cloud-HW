namespace StringCompare
{
    using System.ServiceModel;

    [ServiceContract]
    public interface IStringCompare
    {
        [OperationContract]
        int Contains(string firstString, string secondString);
    }
}