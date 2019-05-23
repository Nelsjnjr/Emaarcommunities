using System;
using EMAAR.Emaarcommunities.Feature.SEO.Interfaces;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Interfaces;
using EMAAR.Emaarcommunities.Foundation.DependencyInjection;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Foundation.Emaarcommunities.Base;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;
using Glass.Mapper.Sc.Web.Mvc;

namespace EMAAR.Emaarcommunities.Feature.SEO.Repositories
{
    /// <summary>
    /// This is used to set SEO functionalities
    /// </summary>
    [Service(typeof(ISeoRepository))]
    public class SeoRepository : ISeoRepository
    {
        #region Property
        private readonly Func<IMvcContext> _mvcContext;
        private readonly ISeoViewModel _seoViewModel ;
        #endregion
        #region constructor
        public SeoRepository(Func<IMvcContext> mvcContext,ISeoViewModel seoViewModel)
        {
            _mvcContext = mvcContext;
            _seoViewModel = seoViewModel;
        }
        #endregion
        #region method
        /// <summary>
        /// Getting page metadata
        /// </summary>
        /// <returns>Metadata/OG/Twitter,Canonical and hreflang</returns>
        public ISeoViewModel GetSeo()
        {
            IMvcContext mvcContext = _mvcContext();
            _seoViewModel.Pagebase= mvcContext.GetContextItem<I_PageBase>();
            _seoViewModel.SiteRoot = mvcContext.GetRootItem<ISiteRoot>(); 
            return _seoViewModel;
        }
        #endregion
    }
}