//-----------------------------------------------------------------------
// <copyright file="Logger.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Core.Logging
{
    using System;

    /// <summary>
    /// The Logger implementation class.
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// The Log field.
        /// </summary>
        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Write an DEBUG message to the Log.
        /// </summary>
        /// <param name="message">The Log message.</param>
        public void Debug(string message)
        {
            Log.Debug(message);
        }

        /// <summary>
        /// Write an INFO message to the Log
        /// </summary>
        /// <param name="message">The Log message.</param>
        public void Info(string message)
        {
            Log.Info(message);
        }

        /// <summary>
        /// Write an WARN message to the Log
        /// </summary>
        /// <param name="message">The Log message.</param>
        public void Warning(string message)
        {
            Log.Warn(message);
        }

        /// <summary>
        /// Write an ERROR message to the Log
        /// </summary>
        /// <param name="message">The Log message.</param>
        public void Error(string message)
        {
            Log.Error(message);
        }

        /// <summary>
        /// Write an ERROR message to the Log.
        /// </summary>
        /// <param name="message">The Log message.</param>
        /// <param name="exception">Exception instance</param>
        public void Error(string message, Exception exception)
        {
            Log.Error(message, exception);
        }

        /// <summary>
        /// Write an FATAL message to the Log.
        /// </summary>
        /// <param name="message">The Log message.</param>
        public void Fatal(string message)
        {
            Log.Fatal(message);
        }
    }
}
