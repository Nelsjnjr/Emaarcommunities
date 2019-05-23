using System;
using System.Collections.Generic;
using System.Linq;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Settings;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Sites;

namespace EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Pipeline
{
	public static class SiteSpecificHttpRequestProcessor 
	{
        /// <summary>
        /// Getting all configured defined in Setting to Run the custom processors/Pipelines
        /// </summary>
        internal static readonly string[] CommunitiesPipelinesExecutableWebsites = SitecoreSettings.CommunitiesPipelinesExecutableWebsites.Split('|');       
		public static bool IsEnabledOnSite(SiteContext site)
		{
			return CommunitiesPipelinesExecutableWebsites.Any(s => s.Equals(site.Name, StringComparison.OrdinalIgnoreCase));
		}

	}
}