using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMAAR.Emaarcommunities.Feature.Navigation.Interface;
using EMAAR.Emaarcommunities.Foundation.DependencyInjection;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;

namespace EMAAR.Emaarcommunities.Feature.Navigation.Models
{
    [Service(typeof(IFooterViewModel))]
    public class FooterViewModel:IFooterViewModel
    {
        public IFooter Footer { get; set; }
        public ISiteRoot SiteRoot { get; set; }

    }
}