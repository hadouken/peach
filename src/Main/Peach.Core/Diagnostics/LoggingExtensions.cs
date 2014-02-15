using System;

namespace Peach.Core.Diagnostics
{
    public static class LoggingExtensions
    {
        public static void Debug(this ILogger logger, string format, params object[] args)
        {
            logger.Write(LogLevel.Debug, String.Format(format, args));
        }

        public static void DebugException(this ILogger logger, string message, Exception exception)
        {
            logger.Write(LogLevel.Debug, message, exception);
        }
    }
}
