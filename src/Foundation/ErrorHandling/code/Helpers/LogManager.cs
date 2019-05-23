//-----------------------------------------------------------------------
// <copyright file="LogManager.cs" company="EMAAR">
//    LogManager.
// </copyright>
// <author>G Naresh Kumar</author>
// <summary>This is the LogManager class.</summary>
//-----------------------------------------------------------------------

namespace EMAAR.Emaarcommunities.Foundation.ErrorHandling.Helpers
{

    # region Using Statements
        using EMAAR.Emaarcommunities.Foundation.ErrorHandling.Interfaces;
        using System;
    #endregion

    /// <summary>
    /// LogManager Class.
    /// </summary>
    public class LogManager :ILogManager
    {
        #region Properties
        /// <summary>
        /// Properties
        /// </summary>       
        readonly log4net.ILog logger = Sitecore.Diagnostics.LoggerFactory.GetLogger("ECMLog");

        #endregion

        #region Methods
        /// <summary>
        /// Method to log debug messages
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="exception">exception.</param>
        public void Debug(string message, Exception exception)
        {
            logger.Debug(message, exception);
        }



        /// <summary>
        /// Method to log error messages
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="exception">exception.</param>
        public void Error(string message, Exception exception)
        {         
            logger.Error(message, exception);
        }



        /// <summary>
        /// Method to log info messages
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="exception">exception.</param>
        public void Info(string message, Exception exception)
        {
            logger.Info(message, exception);
        }


        /// <summary>
        /// Method to log warning messages
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="exception">exception.</param>
        public void Warn(string message, Exception exception)
        {
            logger.Warn(message, exception);
        }

        #endregion
    }
}