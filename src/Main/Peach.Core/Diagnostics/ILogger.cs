using System;

namespace Peach.Core.Diagnostics
{
    public interface ILogger
    {
        void Write(LogLevel level, string message);

        void Write(LogLevel level, string message, Exception exception);
    }
}
