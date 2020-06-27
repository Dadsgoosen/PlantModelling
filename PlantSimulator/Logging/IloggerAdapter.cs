using System;

namespace PlantSimulator.Logging
{
    public interface ILoggerAdapter<T>
    {
        void LogDebug(string message);
        void LogDebug(string message, params object[] args);
        
        void LogInformation(string message);

        void LogInformation(string message, params object[] args);
        
        void LogWarning(string message);
        
        void LogWarning(Exception exception, string message);
        void LogWarning(string message, params object[] args);
        
        void LogError(string message, params object[] args);
        
        void LogError(Exception exception, string message, params object[] args);
        
        void LogFatal(string message);

        void LogFatal(string message, params object[] args);

        void LogFatal(Exception exception, string message);
    }
}