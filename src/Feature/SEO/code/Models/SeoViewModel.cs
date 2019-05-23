using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMAAR.Emaarcommunities.Feature.SEO.Interfaces;
using EMAAR.Emaarcommunities.Foundation.DependencyInjection;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Foundation.Emaarcommunities.Base;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;

namespace EMAAR.Emaarcommunities.Feature.SEO.Models
{
    [Service(typeof(ISeoViewModel))]
    public class SeoViewModel:ISeoViewModel
    {
        public I_PageBase Pagebase { get; set; }
        public ISiteRoot SiteRoot { get; set; }
    }
}