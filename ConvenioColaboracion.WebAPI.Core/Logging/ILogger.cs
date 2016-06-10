//-----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Core.Logging
{
    using System;

    /// <summary>
    /// The File Manager Utility Interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Write an DEBUG message to the log.
        /// </summary>
        /// <param name="message">The Log message.</param>
        void Debug(string message);

        /// <summary>
        /// Write an INFO message to the log
        /// </summary>
        /// <param name="message">The Log message.</param>
        void Info(string message);

        /// <summary>
        /// Write an WARN message to the log
        /// </summary>
        /// <param name="message">The Log message.</param>
        void Warning(string message);

        /// <summary>
        /// Write an ERROR message to the log
        /// </summary>
        /// <param name="message">The Log message.</param>
        void Error(string message);

        /// <summary>
        /// Write an ERROR message to the log.
        /// </summary>
        /// <param name="message">The Log message.</param>
        /// <param name="exception">Exception instance</param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Write an FATAL message to the log.
        /// </summary>
        /// <param name="message">The Log message.</param>
        void Fatal(string message);
    }
}
