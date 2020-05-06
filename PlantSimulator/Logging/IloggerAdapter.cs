using System;

namespace PlantSimulator.Logging
{
    public interface ILoggerAdapter<T>
    {
        void LogDebug(string message);
        
        void LogInformation(string message);

        void LogInformation(string message, params object[] args);
        
        void LogWarning(string message);
        
        void LogWarning(Exception exception, string message);
        
        void LogError(string message);
        
        void LogError(Exception exception, string message);
        
        void LogFatal(string message);

        void LogFatal(Exception exception, string message);
    }
}