namespace ConnectLibrary.Logger
{
    public interface ILogging
    {
       void MakeLog(LoggerOperations op, string message);
    }
}