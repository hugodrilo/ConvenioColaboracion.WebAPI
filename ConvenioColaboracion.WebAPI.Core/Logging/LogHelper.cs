//-----------------------------------------------------------------------
// <copyright file="LogHelper.cs" company="SFP">
//  Copyright (c) 2016 All Rights Reserved
//  <author>Arquitectonet2</author>
// </copyright>
//-----------------------------------------------------------------------

namespace ConvenioColaboracion.WebAPI.Core.Logging
{
    using System.Runtime.CompilerServices;

    /// <summary>
    /// The logger helper class.
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// Gets the named logger by name.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>The requested logger.</returns>
        public static log4net.ILog GetLogger([CallerFilePath]string fileName = "")
        {
            return log4net.LogManager.GetLogger(fileName);
        }
    }
}
