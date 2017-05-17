using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Lab6
{
    static class Logger
    {
        public enum MessageStatus
        {
            NoStatus,
            Info,
            Warning,
            Error,
        };

        public enum MessageFormat
        {
            NoFormatting,
            PrintNewLineAfter,
            PrintNewLineBefore
        }

        /// <summary>
        /// Callback function to execute when new message is came.
        /// </summary>
        private static Action<MessageStatus, string, MessageFormat> LogOutputCallback = default(Action<MessageStatus, string, MessageFormat>);

        public static void SetLogOutputCallback(Action<MessageStatus, string, MessageFormat> callback)
        {
            LogOutputCallback = callback;
        }

        private static void TryCallback(MessageStatus status, string message, MessageFormat format)
        {
            if (LogOutputCallback == default(Action<MessageStatus, string, MessageFormat>))
            {
                MessageBox_Info(message);
                return;
            }

            LogOutputCallback(status, message, format);
        }

        /// <summary>
        /// Prints string without new line after it.
        /// </summary>
        /// <param name="message">String to log</param>
        public static void Print(string message)
        {
            TryCallback(MessageStatus.NoStatus, message, MessageFormat.NoFormatting);
        }

        /// <summary>
        /// Prints string and query new line after it.
        /// </summary>
        /// <param name="message">String to log</param>
        public static void PrintLine(string message)
        {
            TryCallback(MessageStatus.NoStatus, message, MessageFormat.PrintNewLineAfter);
        }

        /// <summary>
        /// Prints string with [INFO] prefix.
        /// </summary>
        /// <param name="message">String to log</param>
        public static void Info(string message)
        {
            TryCallback(MessageStatus.Info, message, MessageFormat.PrintNewLineAfter);
        }

        /// <summary>
        /// Prints string with [WARN] prefix.
        /// </summary>
        /// <param name="message">String to log</param>
        public static void Warning(string message)
        {
            TryCallback(MessageStatus.Warning, message, MessageFormat.PrintNewLineAfter);
        }

        /// <summary>
        /// Prints string with [ERROR] prefix.
        /// </summary>
        /// <param name="message">String to log</param>
        public static void Error(string message)
        {
            TryCallback(MessageStatus.Error, message, MessageFormat.PrintNewLineAfter);
        }

        /// <summary>
        /// Prints string with [ERROR] prefix.
        /// </summary>
        /// <param name="message">String to print</param>
        public static void MessageBox_Error(string message)
        {
            MessageBox.Show(string.Format("[ERROR] {0}", message));
        }

        /// <summary>
        /// Prints string with [ERROR] prefix.
        /// </summary>
        /// <param name="message">String to print</param>
        public static void MessageBox_Info(string message)
        {
            MessageBox.Show(string.Format("[INFO] {0}", message));
        }
    }
}
