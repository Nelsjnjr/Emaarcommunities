//-----------------------------------------------------------------------
// <copyright file="ILogManager.cs" company="EMAAR">
//    ILogManager.
// </copyright>
// <author>G Naresh Kumar</author>
// <summary>This is the ILogManager interface.</summary>
//-----------------------------------------------------------------------

namespace EMAAR.Emaarcommunities.Foundation.ErrorHandling.Interfaces
{
    #region Using Statements
    using System;
    #endregion


    /// <summary>
    /// ILogManager Interface.
    /// </summary>
    interface ILogManager
    {

        #region Methods

        /// <summary>
        /// Method to log debug messages
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="exception">exception.</param>
        void Debug(string message, Exception exception);

        /// <summary>
        /// Method to log info messages
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="exception">exception.</param>
        void Info(string message, Exception exception);

        /// <summary>
        /// Method to log warning messages
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="exception">exception.</param>
        void Warn(string message, Exception exception);

        /// <summary>
        /// Method to log error messages
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="exception">exception.</param>
        void Error(string message, Exception exception);

        #endregion
    }
}
