using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Foundation.Emaarcommunities.Base;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;

namespace EMAAR.Emaarcommunities.Feature.SEO.Interfaces
{
    public interface ISeoViewModel
    {
         I_PageBase Pagebase { get; set; }
         ISiteRoot SiteRoot { get; set; }
    }
}
