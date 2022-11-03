using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rozen.Common
{
    /// <summary>
    ///     Represents a logging message.
    /// </summary>
    public readonly struct LogMessage
    {
        /// <summary>
        ///     The severity of this log.
        /// </summary>
        public LogSeverity Severity { get; }

        /// <summary>
        ///     The message of this log.
        /// </summary>
        public string Message { get; }

        /// <summary>
        ///     The exception of this log.
        /// </summary>
        public Exception? Exception { get; }

        /// <summary>
        ///     Creates a new logmessage from the provided values.
        /// </summary>
        /// <param name="severity"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public LogMessage(LogSeverity severity, string message, Exception? ex = null)
        {
            Severity = severity;
            Message = message;
            Exception = ex;
        }

        /// <summary>
        ///     Creates a verbose log.
        /// </summary>
        /// <param name="verbose"></param>
        /// <returns></returns>
        public static LogMessage FromVerbose(string verbose)
            => new(LogSeverity.Verbose, verbose);

        /// <summary>
        ///     Creates a debug log.
        /// </summary>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static LogMessage FromDebug(string debug)
            => new(LogSeverity.Debug, debug);

        /// <summary>
        ///     Creates an info log.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static LogMessage FromInfo(string info)
            => new(LogSeverity.Information, info);

        /// <summary>
        ///     Creates a warning log.
        /// </summary>
        /// <param name="warning"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static LogMessage FromWarning(string warning, Exception? ex = null)
            => new(LogSeverity.Warning, warning, ex);

        /// <summary>
        ///     Creates an error log.
        /// </summary>
        /// <param name="error"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static LogMessage FromError(string error, Exception? ex = null)
            => new(LogSeverity.Error, error, ex);

        /// <summary>
        ///     Creates a critical error.
        /// </summary>
        /// <param name="critical"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static LogMessage FromCritical(string critical, Exception? ex = null)
            => new(LogSeverity.Critical, critical, ex);
    }
}
