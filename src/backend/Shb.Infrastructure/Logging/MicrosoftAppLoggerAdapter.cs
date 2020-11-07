using System;
using Microsoft.Extensions.Logging;
using Shb.Domain.Abstractions;

namespace Shb.Infrastructure.Logging
{
    internal class MicrosoftAppLoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> logger;
        public MicrosoftAppLoggerAdapter(ILoggerFactory loggerFactory)
        {
            if (loggerFactory is null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            logger = loggerFactory.CreateLogger<T>();
        }

        public void LogCritical(Exception exception, string message, params object[] args) => logger.LogCritical(exception, message, args);
        public void LogDebug(string message, params object[] args) => logger.LogDebug(message, args);
        public void LogError(Exception exception, string message, params object[] args) => logger.LogError(exception, message, args);
        public void LogInformation(string message, params object[] args) => logger.LogInformation(message, args);
        public void LogTrace(string message, params object[] args) => logger.LogTrace(message, args);
        public void LogWarning(string message, params object[] args) => logger.LogWarning(message, args);
    }
}
