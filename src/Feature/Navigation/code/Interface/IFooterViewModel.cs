using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;

namespace EMAAR.Emaarcommunities.Feature.Navigation.Interface
{
    public interface IFooterViewModel
    {
         IFooter Footer { get; set; }
         ISiteRoot SiteRoot { get; set; }
    } 
}
