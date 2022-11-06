using NLog;

namespace API.Services
{
    public class LoggerService : ILoggerService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public void LogError(string message, string? stacktrace)
        {
            _logger.Error(message, stacktrace);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }
    }
}
