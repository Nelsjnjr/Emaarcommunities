//-----------------------------------------------------------------------
// <copyright file="CustomExecuteRequest.cs" company="EMAAR">
//    CustomExecuteRequest.
// </copyright>
// <author>G Naresh Kumar</author>
// <summary>This is the CustomExecuteRequest class.</summary>
//-----------------------------------------------------------------------

namespace EMAAR.Emaarcommunities.Foundation.ErrorHandling.Pipelines.ErrorRedirect
{

    #region Using statements

    using EMAAR.Emaarcommunities.Foundation.ErrorHandling.Helpers;
    using EMAAR.Emaarcommunities.Foundation.ErrorHandling.Interfaces;
    using Sitecore;
    using Sitecore.Abstractions;
    using Sitecore.Configuration;
    using Sitecore.Web;
    using System.Web;

    #endregion
    // From https://www.geekhive.com/buzz/post/2017/07/a-complete-guide-to-configuring-friendly-error-pages-in-sitecore-part-1-404-pages/


    /// <summary>
    /// CustomExecuteRequest Class.
    /// </summary>
    public class CustomExecuteRequest : Sitecore.Pipelines.HttpRequest.ExecuteRequest
	{

        #region Properties
        private readonly BaseLinkManager _baseLinkManager;
		private readonly ILogManager logWrapper;
        #endregion


        #region Methods
        /// <summary>
        /// CustomExecuteRequest constructor method.
        /// </summary>
        public CustomExecuteRequest(BaseSiteManager baseSiteManager, BaseItemManager baseItemManager, BaseLinkManager baseLinkManager) : base(baseSiteManager, baseItemManager)
		{
			_baseLinkManager = baseLinkManager;
            this.logWrapper = new LogManager();
        }

        /// <summary>
        /// PerformRedirect Class.
        /// </summary>
        protected override void PerformRedirect(string url)
		{
			if (Context.Site == null || Context.Database == null || Context.Database.Name == "core")
			{
				logWrapper.Info($"Attempting to redirect url {url}, but no Context Site or DB defined (or core db redirect attempted)",null);
				return;
			}

			// need to retrieve not found item to account for sites utilizing virtualFolder attribute
			var notFoundItem = Context.Database.GetItem(Context.Site.StartPath + Settings.ItemNotFoundUrl);

			if (notFoundItem == null)
			{
				logWrapper.Info($"No 404 item found on site: {Context.Site.Name}",null);
				return;
			}

			var notFoundUrl = _baseLinkManager.GetItemUrl(notFoundItem, new Sitecore.Links.UrlOptions
			{
				AlwaysIncludeServerUrl = false
			});

			if (string.IsNullOrWhiteSpace(notFoundUrl))
			{
				logWrapper.Info($"Found 404 item for site, but no URL returned: {Context.Site.Name}",null);
				return;
			}

			logWrapper.Info($"Redirecting to {notFoundUrl}",null);
			if (Settings.RequestErrors.UseServerSideRedirect)
			{
				HttpContext.Current.Server.TransferRequest(notFoundUrl);
			}
			else
				WebUtil.Redirect(notFoundUrl, false);
		}

        #endregion
    }
}