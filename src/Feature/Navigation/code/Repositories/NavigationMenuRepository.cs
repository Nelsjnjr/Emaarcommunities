#region namespace
using System;
using System.Collections.Generic;
using System.Linq;
using EMAAR.Emaarcommunities.Feature.Navigation.Interface;
using EMAAR.Emaarcommunities.Feature.Navigation.Settings;
using EMAAR.Emaarcommunities.Foundation.DependencyInjection;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Common.Content_Types;
using EMAAR.Emaarcommunities.Foundation.ORM.Models.sitecore.templates.Project.Emaarcommunities.Page_Types;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions;
using EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Interfaces;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data;
using Sitecore.Data.Items;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Navigation.Repositories
{
    /// <summary>
    /// This class is used to provide navigation menu
    /// </summary>
    [Service(typeof(INavigationMenuRepository))]
    public class NavigationMenuRepository : INavigationMenuRepository
    {
        #region Property
        private readonly Func<IMvcContext> _mvcContext;
        private readonly IHeaderViewModel _headerViewModel;
        private readonly ISitecoreHelper _sitecoreHelper;
        private readonly ILeftNavigation _leftNavigation;
        private readonly IFooterViewModel _footerViewModel;
        private readonly ISitemapViewModel _sitemapViewModel;
        #endregion
        #region constructor
        public NavigationMenuRepository(Func<IMvcContext> mvcContext, ISitemapViewModel sitemapViewModel, ILeftNavigation leftNavigation, IFooterViewModel footerViewModel, IHeaderViewModel headerViewModel, IFooter footer, ISitecoreHelper sitecoreHelper)
        {
            _sitecoreHelper = sitecoreHelper;
            _headerViewModel = headerViewModel;
            _mvcContext = mvcContext;
            _leftNavigation = leftNavigation;
            _footerViewModel = footerViewModel;
            _sitemapViewModel = sitemapViewModel;
        }
        #endregion
        #region method
        /// <summary>
        /// Getting Header navigation menu along with logo details for the header
        /// </summary>
        /// <returns>Header details</returns>
        public IHeaderViewModel GetHeader()
        {
            IMvcContext mvcContext = _mvcContext();
            //Checking the current item is the home item to display the Header based on this
            IHome contextItem = mvcContext.GetContextItem<IHome>();
            _headerViewModel.SiteRoot = mvcContext.GetRootItem<ISiteRoot>();
            if (contextItem.TemplateId.ToString().Equals(SitecoreSettings.HomeTemplateID, StringComparison.InvariantCultureIgnoreCase))
            {
                _headerViewModel.SearchIcon = _sitecoreHelper.HomePageSearch;
                _headerViewModel.CloseIcon = _sitecoreHelper.HomePageClose;
                _headerViewModel.HeaderCss = CommonConstants.HomePageHeaderCss;
                _headerViewModel.IsHomePage = true;
            }
            else
            {
                _headerViewModel.SearchIcon = _sitecoreHelper.ContentPageSearch;
                _headerViewModel.CloseIcon = _sitecoreHelper.ContentPageClose;
                _headerViewModel.HeaderCss = CommonConstants.ContentPageHeaderCss;
                _headerViewModel.IsHomePage = false;
            }
            if (contextItem.TemplateId.ToString().Equals(SitecoreSettings.SearchPageTemplateID, StringComparison.InvariantCultureIgnoreCase))
            {
                _headerViewModel.IsSearchPage = true;
            }
            else
                _headerViewModel.IsSearchPage = false;
            _headerViewModel.Header = _sitecoreHelper.NavigationHeader;

            return _headerViewModel;
        }
        /// <summary>
        /// Getting Footer navigation menu along with logo/legal pages and contact info details for the footer
        /// </summary>
        /// <returns>Footer details</returns>
        public IFooterViewModel GetFooter()
        {
            IMvcContext mvcContext = _mvcContext();
            _footerViewModel.Footer = _sitecoreHelper.NavigationFooter;
            _footerViewModel.SiteRoot = mvcContext.GetRootItem<ISiteRoot>();
            return _footerViewModel;
        }
        /// <summary>
        /// Getting left navigation menu from Sitecore content tree based on context item's parent with its childs
        /// Also displays only the items which has the options "Include in Left navigation" selected in sitecore
        /// </summary>
        /// <returns>Left navigation</returns>
        public ILeftNavigation GetLeftNavigation()
        {
            IMvcContext mvcContext = _mvcContext();
            INavigable rootItem = mvcContext.GetContextItem<INavigable>();
            //Setting default to context item
            _leftNavigation.ParentNavigationItem = rootItem;
            _leftNavigation.CurrentNavigationItem = rootItem;
            //Getting current item is Mediacenter based on its template id
            System.Collections.Generic.IEnumerable<Item> mediaCenter = rootItem.ContextItem.Axes.GetAncestors().Where(p => ID.Parse(p.TemplateID.ToGuid()).Equals(ID.Parse(SitecoreSettings.MediaCenterTemplateID)));
            if (mediaCenter.Any())
            {
                //If context item is child of media center item template then have parent has Mediacenter
                _leftNavigation.ParentNavigationItem = mvcContext.SitecoreService.GetItem<INavigable>(mediaCenter.FirstOrDefault().ID.ToGuid());
                //All nested childs will always have CurrentItemNavigation has its Mediacenter immediate child from where its(context item) comes from. 
                IEnumerable<Item> currentItem = mediaCenter.FirstOrDefault().Children.Where(p => rootItem.ContextItem.Paths.LongID.Contains(p.ID.ToString())).Select(p => p);
                if (currentItem.Any())
                {
                    _leftNavigation.CurrentNavigationItem = _mvcContext()?.SitecoreService.GetItem<INavigable>(currentItem.FirstOrDefault().ID.ToGuid());
                }
            }
            else
            {
                //Setting Current navigation item to context item
                _leftNavigation.CurrentNavigationItem = rootItem;
            }
            //Getting Parent item based on the following templates (Mediacenter,Generic contentpage and contentpage)
            switch (rootItem)
            {
                case var template when ID.Parse(rootItem.TemplateId).Equals(ID.Parse(SitecoreSettings.MediaCenterTemplateID)):
                    _leftNavigation.ParentNavigationItem = _mvcContext()?.SitecoreService.GetItem<INavigable>(rootItem.Id);
                    break;
                case var template when rootItem.BaseTemplateId.AsEnumerable().Any(p => p.Equals(SitecoreSettings.GenericContentPageTemplateID))
                     || ID.Parse(rootItem.TemplateId).Equals(ID.Parse(SitecoreSettings.GenericContentPageTemplateID)):
                    _leftNavigation.ParentNavigationItem = _mvcContext()?.SitecoreService.GetItem<INavigable>(rootItem.Parent.Id);
                    break;
                case var template when rootItem.BaseTemplateId.AsEnumerable().Any(p => p.Equals(SitecoreSettings.GenericContentRootPageTemplateID))
                     || ID.Parse(rootItem.TemplateId).Equals(ID.Parse(SitecoreSettings.GenericContentRootPageTemplateID)):
                    _leftNavigation.ParentNavigationItem = rootItem;
                    break;
                default:
                    break;
            }
            return _leftNavigation;
        }
        /// <summary>
        /// Generating Sitemap data from Header navigation datasource under(/Sitecontent/Navigation/Header)
        /// </summary>
        /// <returns>Sitemap datasource out of Header navigation items </returns>
        public ISitemapViewModel GetSitemap()
        {           
            _sitemapViewModel.Sitemap = _sitecoreHelper.Sitemap;
            return _sitemapViewModel;
        }
        #endregion



    }
}