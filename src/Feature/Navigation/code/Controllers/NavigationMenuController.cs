#region namespace
using System.Web.Mvc;
using EMAAR.Emaarcommunities.Feature.Navigation.Interface;
using static EMAAR.Emaarcommunities.Foundation.SitecoreExtensions.Settings.SitecoreSettings;
#endregion
namespace EMAAR.Emaarcommunities.Feature.Navigation.Controllers
{
    public class NavigationMenuController : Controller
    {
        #region Private property
        private readonly INavigationMenuRepository _navigationMenuRepository;
        #endregion
        #region construtor    
        public NavigationMenuController(INavigationMenuRepository navigationMenuRepository)
        {
            _navigationMenuRepository = navigationMenuRepository;
        }
        #endregion
        #region method
        /// <summary>
        /// Getting Header with all necessary details like(Menus,Logo,Search text,Login Text etc)
        /// </summary>
        /// <returns>Header</returns>
        public ActionResult GetHeader()
        {
            return View($"{ViewPath}NavigationMenu/_Header.cshtml", _navigationMenuRepository.GetHeader());
        }
        /// <summary>
        ///  Getting Footer with all necessary details like(Menus,Logo,Contact Info and Legalpages etc)
        /// </summary>
        /// <returns>Footer</returns>
        public ActionResult GetFooter()
        {
            return View($"{ViewPath}NavigationMenu/_Footer.cshtml", _navigationMenuRepository.GetFooter());
        }
        /// <summary>
        /// Getting left navigation menu from Sitecore content tree based on context item's parent with its childs
        /// Also displays only the items which has the options "Include in Left navigation" selected in sitecore
        /// </summary>
        /// <returns>Left navigation</returns>
        public ActionResult GetLeftNavigation()
        {
            return View($"{ViewPath}NavigationMenu/_LeftNavigation.cshtml", _navigationMenuRepository.GetLeftNavigation());
        }
        public ActionResult GetSitemap()
        {

            return View($"{ViewPath}NavigationMenu/_Sitemap.cshtml", _navigationMenuRepository.GetSitemap());
        }
        #endregion
    }
}