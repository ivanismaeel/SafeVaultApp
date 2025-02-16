public static class LoggerHelper
{
    public static void LogError(string message, Exception ex)
    {
        // Implement logging mechanism (e.g., log to a file, database, or external service)
        Console.WriteLine($"Error: {message}\nException: {ex.Message}");
    }
}
