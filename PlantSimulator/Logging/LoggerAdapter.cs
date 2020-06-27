using System;
using Microsoft.Extensions.Logging;

namespace PlantSimulator.Logging
{
    public class LoggerAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger<T> logger;

        public LoggerAdapter(ILogger<T> logger)
        {
            this.logger = logger;
        }

        public void LogDebug(string message)
        {
            logger.LogDebug(message);
        }

        public void LogDebug(string message, params object[] args)
        {
            logger.LogDebug(message, args);
        }

        public void LogInformation(string message)
        {
            logger.LogInformation(message);
        }

        public void LogInformation(string message, params object[] args)
        {
            logger.LogInformation(message, args);
        }

        public void LogWarning(string message)
        {
            logger.LogWarning(message);
        }

        public void LogWarning(Exception exception, string message)
        {
            logger.LogWarning(exception, message);
        }

        public void LogWarning(string message, params object[] args)
        {
            logger.LogWarning(message, args);
        }

        public void LogError(string message, params object[] args)
        {
            logger.LogError(message, args);
        }

        public void LogError(Exception exception, string message, params object[] args)
        {
            logger.LogError(exception, message, args);
        }

        public void LogFatal(string message)
        {
            logger.LogCritical(message);
        }

        public void LogFatal(string message, params object[] args)
        {
            logger.LogCritical(message, args);
        }

        public void LogFatal(Exception exception, string message)
        {
            logger.LogCritical(exception, message);
        }
    }
}