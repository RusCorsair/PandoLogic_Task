namespace API.Services
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogError(string message, string? stacktrace);
    }
}
