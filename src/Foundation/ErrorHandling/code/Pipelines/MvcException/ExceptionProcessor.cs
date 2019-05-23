//-----------------------------------------------------------------------
// <copyright file="ExceptionProcessor.cs" company="EMAAR">
//    ExceptionProcessor.
// </copyright>
// <author>G Naresh Kumar</author>
// <summary>This is the ExceptionProcessor class.</summary>
//-----------------------------------------------------------------------

namespace EMAAR.Emaarcommunities.Foundation.ErrorHandling.Pipelines.MvcException
{

    #region Using Statements
    using Sitecore.Configuration;
    using Sitecore.Mvc.Pipelines.MvcEvents.Exception;
    using Sitecore.Web;
    using System.Web;
    using System.Web.Mvc;
    #endregion

    /// <summary>
    /// ExceptionProcessor Class.
    /// </summary>
    public class ExceptionProcessor
    {

        #region Properties
        log4net.ILog logger = Sitecore.Diagnostics.LoggerFactory.GetLogger("ECMLog");
        #endregion

        #region Methods
        public void Process(ExceptionArgs exceptionArgs)
        {
            if (exceptionArgs.ExceptionContext.ExceptionHandled)
            {
                return;
            }

            this.HandleException(exceptionArgs.ExceptionContext);
        }

        protected virtual void HandleException(ExceptionContext exceptionContext)
        {
           
           logger.Error(exceptionContext.Exception.Message, exceptionContext.Exception);

           if (Settings.RequestErrors.UseServerSideRedirect)
            {
                HttpContext.Current.Server.TransferRequest("~/error");
            }
            else
                WebUtil.Redirect("~/error", false);
   
            
        }
        #endregion  
    }
}