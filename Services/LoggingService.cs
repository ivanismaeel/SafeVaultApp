using Serilog;

public class LoggingService
{
    public void LogAccess(string message)
    {
        Log.Information(message);
    }
}
